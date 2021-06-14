using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.CoreDtos;
using Core.DataFilters;
using Core.Entities;
using Core.Enumerations;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data.Services
{


    public class BillService : IBillService
    {
        private readonly ITransactionService _transactionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BillService> _logger;

        public BillService(ITransactionService transactionService, IUnitOfWork unitOfWork,ILogger<BillService> logger)
        {
            _transactionService = transactionService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IReadOnlyList<InmateBill>> GetInmateBillsAsync(BillFilter filter)
        {
            var spec = new BillForInmateWithBillDetailsSpecification(filter);
            return await _unitOfWork.Repository<InmateBill>().FindAllBySpecAsync(spec);
        }
        public async Task<int> GetInmateBillsCountAsync(BillFilter filter)
        {
            var spec = new BillForInmateWithBillDetailsSpecification(filter, true);
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

            var inmate = await _unitOfWork.Repository<Inmate>().FindByIdAsync(paymentDto.InmateId);
            inmate.AmountDue -= paymentDto.Amount;
            if (inmate.AmountDue < 0)
            {
                inmate.Savings = -1 * inmate.AmountDue;
                inmate.AmountDue = 0;
            }

            var associatedVendorNamesList = new List<string> { VendorNames.Main, VendorNames.BillPaymentAccount };

            var vendorSpec = new VendorSpecification(associatedVendorNamesList);

            var vendors = await _unitOfWork.Repository<Vendor>().FindAllBySpecAsync(vendorSpec);

            var mainAccount = vendors.FirstOrDefault(v => v.Name == VendorNames.Main);

            var billPaymentAccount = vendors.FirstOrDefault(v => v.Name == VendorNames.BillPaymentAccount);

            var transaction = new TransactionDetail
            {

                Name = $"Bill Payment-{paymentDto.PaidOn.Value.Month}- {inmate.FullName}",
                CategoryId = 4,
                PaidPartyId = billPaymentAccount.Id,
                PaidToId = mainAccount.Id ,
                Amount = paymentDto.Amount,
                IsExpense = true,
                TransactionDate = DateTime.Now
            };

            await _transactionService.PostTransaction(transaction);

            if (paymentDto.BillId.HasValue && paymentDto.BillId != 0)
            {
                var bill = await _unitOfWork.Repository<InmateBill>()
                    .FindOneBySpecAsync(new BillSpecification(paymentDto.BillId.Value));

                if (bill != null)
                {
                    if (bill.BillItems.Any(b => b.ItemCategoryName == BillCategory.DEPOSIT.ToString()))
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
        public async Task<MonthlyBillDto> GenerateMonthlyBillAsync(int month, int year,bool simulated = true)
        {
            if (!simulated) {

                var billSpec = new BillSpecification(month, year);
                var inmateBillsForMonth = await _unitOfWork.Repository<InmateBill>().FindAllBySpecAsync(billSpec);

                _unitOfWork.Repository<InmateBill>().RemoveMany(inmateBillsForMonth);

                var transactionsToDeleteSpec = new TransactionDetailSpecification(month, year, true);
                var transactionsToDelete = await _unitOfWork.Repository<TransactionDetail>().FindAllBySpecAsync(transactionsToDeleteSpec);

                _unitOfWork.Repository<TransactionDetail>().RemoveMany(transactionsToDelete);

                await _unitOfWork.Complete();
            }

            var filter = new TransactionsFilter(month, year, false)
            {
                IsAutoGeneratedTxnsNeeded = false
            };
            var spec = new TransactionWithCategoryAndVendorSpecification(filter);
            var transactions = await _unitOfWork.Repository<TransactionDetail>().FindAllBySpecAsync(spec);
            var transactionsGrouped = transactions.GroupBy(t => t.CategoryId);

            var categoricalSum = transactionsGrouped.ToDictionary(billCategory => billCategory.Key, billCategory => billCategory.Sum(b => b.Amount));

            var monthlyBill = new MonthlyBillDto(month, year);

            var categories = await _unitOfWork.Repository<Category>().FindAllAsync();

       

            var categoryWiseExpensesList = new List<CategoryWiseExpense>();

            decimal monthlyTotal = 0;

            var associatedVendorNamesList = new List<string> { VendorNames.Main, VendorNames.Others };

            var vendorSpec = new VendorSpecification(associatedVendorNamesList);

            var vendors = await _unitOfWork.Repository<Vendor>().FindAllBySpecAsync(vendorSpec);

            var mainAccount = vendors.FirstOrDefault(v => v.Name == VendorNames.Main);

            var othersAccount = vendors.FirstOrDefault(v => v.Name == VendorNames.Others);


            foreach (var category in categories)
            {
                var categoryWiseExpense = new CategoryWiseExpense
                {
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    TotalAmount = category.ConsiderDefaultRate && category.DefaultRate != 0 ? category.DefaultRate : (categoricalSum.ContainsKey(category.Id)?categoricalSum[category.Id]:0),
                    TransactionDetails = transactions
                            .Where(t => t.Category.Name == category.Name)
                            .Select(t => new TransactionDetailDto()
                            {
                                TransactionDetailName = t.Name,
                                Amount = t.Amount
                            }).ToList()
                };

                if(categoryWiseExpense.TransactionDetails == null || categoryWiseExpense.TransactionDetails.Count == 0)
                {
                    if (!simulated && categoryWiseExpense.TotalAmount > 0) {
                        var transaction = new TransactionDetail
                        {
                            Name = categoryWiseExpense.CategoryName,
                            CategoryId = category.Id,
                            PaidPartyId = mainAccount.Id,
                            PaidToId = othersAccount.Id,
                            Amount = categoryWiseExpense.TotalAmount,
                            TransactionDate = new DateTime(year,month,1),
                            IsExpense = true,
                            IsAutoGenerated = true
                        };

                        await _transactionService.PostTransaction(transaction);
                    }                    

                    categoryWiseExpense.TransactionDetails = new List<TransactionDetailDto>
                    {
                        new TransactionDetailDto
                        {
                            TransactionDetailName = categoryWiseExpense.CategoryName,
                            Amount = categoryWiseExpense.TotalAmount
                        }
                    };
                }

                monthlyTotal += categoryWiseExpense.TotalAmount;

                if(category.Name != BillCategory.DEPOSIT.ToString() && category.Name != BillCategory.BILLPAYMENT.ToString())

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

            if(inmates.Count == 0)
            {
                throw new Exception("No inmates present");
            }

            var categories = await _unitOfWork.Repository<Category>().FindAllAsync();

            var monthlyBillDetails = await GenerateMonthlyBillAsync(month, year,false);

            var txnDetails = monthlyBillDetails.CategoryWiseExpenses;

            var messTransactions = txnDetails.FirstOrDefault(c => c.CategoryName == BillCategory.MESS.ToString());
            var otherTransactions = txnDetails.Where(c => c.CategoryName != BillCategory.RENT.ToString() && c.CategoryName != BillCategory.MESS.ToString()).ToList();
            var categoryNames = otherTransactions.Select(o => o.CategoryName).Distinct();


            var bottomBedInmates = inmates.Where(i => !i.IsInmateOnTopBed).Count();
            var visitorsCount = inmates.Count(i => i.IsVisit);

            var defaultRent = txnDetails.Where(c => c.CategoryName == BillCategory.RENT.ToString()).Sum(i => i.TotalAmount);

            _logger.LogInformation($"Generating rent For top with  bottomBedInmates : {bottomBedInmates} , inmatesCount : {inmates?.Count} for default rate : {defaultRent}");

            var rentforTop = GetRentForBottom(bottomBedInmates, inmates.Count, defaultRent);

            var occuppencies = CalculateOccuppancy(inmates, month, year);

            var totalOccuppancy = occuppencies[0];

            foreach (var inmate in inmates)
            {
                var inmateBill = new InmateBill
                {
                    InmateId = inmate.Id,
                    Month = month,
                    Year = year,
                    CreatedOn = DateTime.Now
                };
                var billDetails = new List<BillDetail>();  
             
                var rentDetail = new BillDetail("RENT", (int)BillCategory.RENT, BillCategory.RENT.ToString(), rentforTop, inmateBill);

                billDetails.Add(rentDetail);

                if (!inmate.IsInmateOnTopBed)
                {
                    rentDetail.Amount = (decimal)rentforTop + 30;
                }

                var messExpense = messTransactions.TotalAmount;

                var occuppancy = occuppencies[inmate.Id];

                var amountForInmate = messExpense * (occuppancy/totalOccuppancy);

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
                            //check this
                            amountForInmate = cat.NeedToConsiderDays ? (decimal)(expense / inmates.Count - visitorsCount) * (occuppancy/totalOccuppancy) : (expense / (inmates.Count - visitorsCount));
                        }
                        else
                        {
                            amountForInmate = cat.NeedToConsiderDays ? (decimal) expense * (occuppancy / totalOccuppancy) : (expense / inmates.Count);
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
        private static decimal GetRentForBottom(int x, int y, decimal rent)
        {
            return (rent - x * 30) / y;
        }

        private static Dictionary<int,decimal> CalculateOccuppancy(IEnumerable<Inmate> inmates , int month ,int year)
        {
            var firstDayOfMonth = new DateTime(year, month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            int numberOfDaysInMonth = (lastDayOfMonth - firstDayOfMonth).Days + 1;
            decimal totalOccuppancy = 0;

            Dictionary<int, decimal> occupancies = new Dictionary<int, decimal>();

            foreach (var inmate in inmates)
            {
                decimal occuppancy = 1;
                int numberOfLeaveDays = 0;
                var leavesForInmate = inmate.InmateLeaves.Where(x => x.Status != LeaveStatus.Cancelled || x.Status != LeaveStatus.Rejected);
                if (leavesForInmate.Any())
                {
                    var leaveDaysInMonth = leavesForInmate.Where(l =>
                        (l.FromDate >= firstDayOfMonth && l.FromDate <= lastDayOfMonth) || (l.ToDate <= lastDayOfMonth && l.ToDate >= firstDayOfMonth));

                    foreach (var leave in leaveDaysInMonth)
                    {
                        if (leave.ToDate <= lastDayOfMonth && leave.FromDate >= firstDayOfMonth)
                            numberOfLeaveDays += (leave.ToDate - leave.FromDate).Days + 1;
                        else if (leave.FromDate < firstDayOfMonth && leave.ToDate <= lastDayOfMonth)
                            numberOfLeaveDays += (leave.ToDate - firstDayOfMonth).Days + 1;
                        else if (leave.ToDate > lastDayOfMonth && leave.FromDate >= firstDayOfMonth)
                            numberOfLeaveDays += (lastDayOfMonth - leave.FromDate).Days + 1;
                        else if (leave.ToDate > lastDayOfMonth && leave.FromDate < firstDayOfMonth)
                            numberOfLeaveDays += (lastDayOfMonth - firstDayOfMonth).Days + 1;

                    }

                }
                occuppancy = ((numberOfDaysInMonth - numberOfLeaveDays) / (decimal)numberOfDaysInMonth);
                occupancies.Add(inmate.Id, occuppancy);
                totalOccuppancy += occuppancy;
            }
            occupancies.Add(0, totalOccuppancy);

            return occupancies;
        }

        //private void GenerateInvoices(int month , int year)
        //{
        //    var fileName = $"Bill-{month}-{year}.xlsx";
        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "content", fileName);

        //    FileInfo file = new FileInfo(filePath);

        //    if (file.Exists)
        //    {
        //        file.Delete();
        //        file = new FileInfo(filePath);
        //    }

        //    using var package = new ExcelPackage(file);

        //    ExcelWorksheet ws = package.Workbook.Worksheets.Add("Bill Details");

        //    ws.Cells["A1:I2"].Merge = true;




        //}
    }
}