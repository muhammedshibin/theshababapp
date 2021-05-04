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

        public BillService(ITransactionService transactionService,IUnitOfWork unitOfWork)
        {
            _transactionService = transactionService;
            _unitOfWork = unitOfWork;
        }
        public async Task<MonthlyBillDto> GenerateMonthlyBillAsync(int month, int year)
        {
            var billSpec = new BillSpecification(month, year);
            var inmateBillsForMonth = await _unitOfWork.Repository<InmateBill>().FindAllBySpecAsync(billSpec);
            
            _unitOfWork.Repository<InmateBill>().RemoveMany(inmateBillsForMonth);

            await _unitOfWork.Complete();
            
            var filter = new TransactionsFilter(month, year,false);
            var transactions = await  _transactionService.GetTransactions(filter);
            var transactionsGrouped = transactions.GroupBy(t => t.CategoryId);

            var categoricalSum = transactionsGrouped.ToDictionary(billCategory => billCategory.Key, billCategory => billCategory.Sum(b => b.Amount));

            var monthlyBill = new MonthlyBillDto(month,year);

            var categories = transactions.Select(c => c.Category).Distinct().ToList();

            var categoryWiseExpensesList = new List<CategoryWiseExpense>();

            double monthlyTotal = 0;

            foreach (var category in categories)
            {
                var categoryWiseExpense = new CategoryWiseExpense
                {
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    TotalAmount = categoricalSum[category.Id],
                    TransactionDetails = transactions.Select(t => new TransactionDetailDto()
                    {
                        TransactionDetailName = t.Name, Amount = t.Amount
                    }).ToList()
                };

                monthlyTotal += categoryWiseExpense.TotalAmount;
                
                categoryWiseExpensesList.Add(categoryWiseExpense);
            }

            monthlyBill.CategoryWiseExpenses = categoryWiseExpensesList;

            monthlyBill.SubTotal = monthlyTotal;

            return monthlyBill;
        }
        public async Task GenerateIndividualBillsAsync(int month , int year)
        {
            var inmateBills = new List<InmateBill>();

            var inmatesSpec = new InmateWithLeaveSpecification();
            var inmates = await  _unitOfWork.Repository<Inmate>().FindAllBySpecAsync(inmatesSpec);

            var categories = await _unitOfWork.Repository<Category>().FindAllAsync();

            var monthlyBillDetails = await GenerateMonthlyBillAsync(month, year);
            var messTransactions = monthlyBillDetails.CategoryWiseExpenses.FirstOrDefault(c => c.CategoryName == BillCategory.MESS.ToString());
            var otherTransactions = monthlyBillDetails.CategoryWiseExpenses.Where(c => c.CategoryName != BillCategory.RENT.ToString() && c.CategoryName == BillCategory.MESS.ToString()).ToList();
            var categoryNames = otherTransactions.Select(o => o.CategoryName).Distinct();
            
            
            var bottomBedInmates = inmates.Where(i => !i.IsInmateOnTopBed).Count();

            var defaultRent = categories.FirstOrDefault(c => c.Name == BillCategory.RENT.ToString()).DefaultRate;

            var rentforTop = GetRentForBottom(bottomBedInmates, inmates.Count, defaultRent);

            var firstDayOfMonth = new DateTime(year, month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            double numberOfDaysInMonth = (lastDayOfMonth - firstDayOfMonth).Days;

            foreach (var inmate in inmates)
            {
                var inmateBill = new InmateBill();
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

                var rentDetail = new BillDetail("Rent", (int)BillCategory.RENT, BillCategory.RENT.ToString(),rentforTop, inmateBill);

                billDetails.Add(rentDetail);

                if (!inmate.IsInmateOnTopBed)
                {
                    rentDetail.Amount = rentforTop + 30;
                }

                var messExpense = messTransactions.TotalAmount;

                var amountForInmate = (messExpense / inmates.Count) * occuppancy;

                var messDetail = new BillDetail("Mess", (int)BillCategory.MESS, BillCategory.MESS.ToString(), amountForInmate, inmateBill);

                billDetails.Add(messDetail);

                //other expenses
                foreach (var category in categoryNames)
                {
                    var cat = categories.FirstOrDefault(c => c.Name == category);

                    var thisCategoryDetails = otherTransactions.Where(t => t.CategoryName == category).FirstOrDefault();

                    var expense = thisCategoryDetails.TotalAmount;

                    if(!cat.IsApplicableForVisitors && inmate.IsVisit)
                    {
                        continue;
                    }
                    else
                    {
                        amountForInmate = cat.NeedToConsiderDays ? (expense / inmates.Count) * occuppancy : (expense / inmates.Count);

                        var thisCategoryBillDetail = new BillDetail(category, cat.Id, category, amountForInmate, inmateBill);

                        billDetails.Add(thisCategoryBillDetail);
                    }

                }

                if (!inmate.IsVisit)
                {
                    var depositCategory = categories.FirstOrDefault(c => c.Name == BillCategory.DEPOSIT.ToString());
                    var depositBill = new BillDetail("DEPOSIT", BillCategory.DEPOSIT.GetHashCode(),BillCategory.DEPOSIT.ToString(), depositCategory.DefaultRate, inmateBill);
                    billDetails.Add(depositBill);
                }

                inmateBill.BillItems = billDetails;

                inmateBills.Add(inmateBill);

                _unitOfWork.Repository<InmateBill>().AddMany(inmateBills);

                await _unitOfWork.Complete();
            }
        }

        public double GetRentForBottom(int x, int y, double rent)
        {
            return (rent - x * 30) / y;
        }
    }
}