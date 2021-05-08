using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CoreDtos;
using Core.DataFilters;
using Core.Entities;
using Core.Enumerations;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Data.Services
{


    public class BillService : IBillService
    {
        private readonly ITransactionService _transactionService;
        private readonly IUnitOfWork _unitOfWork;

        public BillService(ITransactionService transactionService, IUnitOfWork unitOfWork)
        {
            _transactionService = transactionService;
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<InmateBill>> GetInmateBillsAsync(BillFilter filter)
        {
            var spec = new BillForInmateWithBillDetailsSpecification(filter);
            return await _unitOfWork.Repository<InmateBill>().FindAllBySpecAsync(spec);
        }
        public async Task<int> GetInmateBillsCountAsync(BillFilter filter)
        {
            var spec = new BillForInmateWithBillDetailsSpecification(filter,true);
            return await _unitOfWork.Repository<InmateBill>().GetCountForSpecAsync(spec);
        }
        public async Task<IReadOnlyList<InmateBill>> GetBillsFOrInmateAsync(int inmateId)
        {
            var billspec = new BillForInmateWithBillDetailsSpecification(inmateId);
            return await _unitOfWork.Repository<InmateBill>().
                FindAllBySpecAsync(billspec);
        }
        public async Task<bool> AcceptBillPayment(PaymentDto paymentDto)
        {
            var payment = new BillPayment
            {
                InmateId = paymentDto.InmateId,
                Amount = paymentDto.Amount,
                BillId = paymentDto.BillId.HasValue ? paymentDto.BillId : null,
                PaidOn = paymentDto.PaidOn ?? DateTime.Now
            };

            

            var vendor = await _unitOfWork.Repository<Vendor>().FindByIdAsync(1);

            vendor.AmountInHand += paymentDto.Amount;

            var inmate = await _unitOfWork.Repository<Inmate>().FindByIdAsync(paymentDto.InmateId);
            inmate.AmountDue -= paymentDto.Amount;
            if(inmate.AmountDue < 0)
            {
                inmate.Savings = -1 * inmate.AmountDue;
                inmate.AmountDue = 0;                
            }
            
            if (paymentDto.BillId.HasValue && paymentDto.BillId != 0)
            {
                var bill = await _unitOfWork.Repository<InmateBill>()
                    .FindOneBySpecAsync(new BillSpecification(paymentDto.BillId.Value));

                if(bill != null)
                {
                    if (bill.BillItems.Any(b => b.ItemCategoryName == "DEPOSIT"))
                    {
                        inmate.Savings += 100;
                    }

                    bill.BillPayment = payment;

                    if (paymentDto.Amount - bill.BillAmount >= 0)
                    {
                        bill.PaymentStatus = PaymentStatus.Paid;
                    }
                    if (paymentDto.Amount < bill.BillAmount)
                    {
                        bill.PaymentStatus = PaymentStatus.PartiallyPaid;
                    }

                }
                
            }
            else
            {
                _unitOfWork.Repository<BillPayment>().Add(payment);
            }

            return await _unitOfWork.Complete() > 0;

        }
        public async Task<MonthlyBillDto> GenerateMonthlyBillAsync(int month, int year)
        {
            var billSpec = new BillSpecification(month, year);
            var inmateBillsForMonth = await _unitOfWork.Repository<InmateBill>().FindAllBySpecAsync(billSpec);

            _unitOfWork.Repository<InmateBill>().RemoveMany(inmateBillsForMonth);

            await _unitOfWork.Complete();

            var filter = new TransactionsFilter(month, year, false);
            var transactions = await _transactionService.GetTransactions(filter);
            var transactionsGrouped = transactions.GroupBy(t => t.CategoryId);

            var categoricalSum = transactionsGrouped.ToDictionary(billCategory => billCategory.Key, billCategory => billCategory.Sum(b => b.Amount));

            var monthlyBill = new MonthlyBillDto(month, year);

            var categories = transactions.Select(c => c.Category).ToHashSet();

            var categoryWiseExpensesList = new List<CategoryWiseExpense>();

            double monthlyTotal = 0;

            foreach (var category in categories)
            {
                var categoryWiseExpense = new CategoryWiseExpense
                {
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    TotalAmount = categoricalSum[category.Id],
                    TransactionDetails = transactions
                            .Where(t => t.Category.Name == category.Name)
                            .Select(t => new TransactionDetailDto()
                            {
                                TransactionDetailName = t.Name,
                                Amount = t.Amount
                            }).ToList()
                };

                monthlyTotal += categoryWiseExpense.TotalAmount;

                categoryWiseExpensesList.Add(categoryWiseExpense);
            }

            monthlyBill.CategoryWiseExpenses = categoryWiseExpensesList;

            monthlyBill.SubTotal = monthlyTotal;

            return monthlyBill;
        }
        public async Task GenerateIndividualBillsAsync(int month, int year)
        {
            var inmateBills = new List<InmateBill>();

            var inmatesSpec = new InmateWithLeaveSpecification();
            var inmates = await _unitOfWork.Repository<Inmate>().FindAllBySpecAsync(inmatesSpec);

            var categories = await _unitOfWork.Repository<Category>().FindAllAsync();

            var monthlyBillDetails = await GenerateMonthlyBillAsync(month, year);

            var txnDetails = monthlyBillDetails.CategoryWiseExpenses;

            var messTransactions = txnDetails.FirstOrDefault(c => c.CategoryName == BillCategory.MESS.ToString());
            var otherTransactions = txnDetails.Where(c => c.CategoryName != BillCategory.RENT.ToString() && c.CategoryName != BillCategory.MESS.ToString()).ToList();
            var categoryNames = otherTransactions.Select(o => o.CategoryName).Distinct();


            var bottomBedInmates = inmates.Where(i => !i.IsInmateOnTopBed).Count();
            var visitorsCount = inmates.Count(i => i.IsVisit);
            //check this later
            //var defaultRentSettings = categories.FirstOrDefault(c => c.Name == BillCategory.RENT.ToString());

            var defaultRent = txnDetails.FirstOrDefault(t => t.CategoryName == "RENT").TotalAmount;

            var rentforTop = GetRentForBottom(bottomBedInmates, inmates.Count, defaultRent);

            var firstDayOfMonth = new DateTime(year, month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            double numberOfDaysInMonth = (lastDayOfMonth - firstDayOfMonth).Days;

            foreach (var inmate in inmates)
            {
                var inmateBill = new InmateBill();
                inmateBill.InmateId = inmate.Id;
                inmateBill.Month = month;
                inmateBill.Year = year;
                inmateBill.CreatedOn = DateTime.Now;
                var billDetails = new List<BillDetail>();
                var leavesForInmate = inmate.InmateLeaves;

                double occuppancy = 1;
                int numberOfLeaveDays = 0;
                if (leavesForInmate.Any())
                {
                    var leaveDaysInMonth = leavesForInmate.Where(l =>
                        (l.FromDate >= firstDayOfMonth) || (l.ToDate <= lastDayOfMonth));

                    foreach (var leave in leaveDaysInMonth)
                    {
                        if (leave.ToDate < lastDayOfMonth)
                            numberOfLeaveDays += (leave.ToDate - leave.FromDate).Days;
                        if (leave.FromDate < firstDayOfMonth)
                            numberOfLeaveDays += (leave.ToDate - firstDayOfMonth).Days;
                        if (leave.ToDate > lastDayOfMonth)
                            numberOfLeaveDays += (lastDayOfMonth - leave.FromDate).Days;
                    }

                }
                occuppancy = (numberOfDaysInMonth - numberOfLeaveDays) / numberOfDaysInMonth;

                var rentDetail = new BillDetail("RENT", (int)BillCategory.RENT, BillCategory.RENT.ToString(), rentforTop, inmateBill);

                billDetails.Add(rentDetail);

                if (!inmate.IsInmateOnTopBed)
                {
                    rentDetail.Amount = rentforTop + 30;
                }

                var messExpense = messTransactions.TotalAmount;

                var amountForInmate = (messExpense / inmates.Count) * occuppancy;

                var messDetail = new BillDetail("MESS", (int)BillCategory.MESS, BillCategory.MESS.ToString(), amountForInmate, inmateBill);

                billDetails.Add(messDetail);

                //other expenses
                foreach (var category in categoryNames)
                {
                    var cat = categories.FirstOrDefault(c => c.Name == category);

                    var thisCategoryDetails = otherTransactions.Where(t => t.CategoryName == category).FirstOrDefault();

                    var expense = thisCategoryDetails.TotalAmount;

                    if (!cat.IsApplicableForVisitors && inmate.IsVisit)
                    {
                        continue;
                    }
                    else
                    {

                        if (!cat.IsApplicableForVisitors && (inmates.Count > visitorsCount))
                        {
                            amountForInmate = cat.NeedToConsiderDays ? (expense / inmates.Count - visitorsCount) * occuppancy : (expense / (inmates.Count - visitorsCount));
                        }
                        else
                        {
                            amountForInmate = cat.NeedToConsiderDays ? (expense / (inmates.Count)) * occuppancy : (expense / inmates.Count);
                        }


                        var thisCategoryBillDetail = new BillDetail(category, cat.Id, category, amountForInmate, inmateBill);

                        billDetails.Add(thisCategoryBillDetail);
                    }

                }

                if (!inmate.IsVisit)
                {
                    var depositCategory = categories.FirstOrDefault(c => c.Name == BillCategory.DEPOSIT.ToString());
                    var depositBill = new BillDetail("DEPOSIT", BillCategory.DEPOSIT.GetHashCode(), BillCategory.DEPOSIT.ToString(), depositCategory.DefaultRate, inmateBill);
                    billDetails.Add(depositBill);
                }

                inmateBill.BillItems = billDetails;

                inmateBill.BillAmount = billDetails.Sum(i => i.Amount);

                inmate.AmountDue += inmateBill.BillAmount;

                inmateBills.Add(inmateBill);
            }

            _unitOfWork.Repository<InmateBill>().AddMany(inmateBills);
            await _unitOfWork.Complete();


        }
        private static double GetRentForBottom(int x, int y, double rent)
        {
            return (rent - x * 30) / y;
        }
    }
}