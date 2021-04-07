using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CoreDtos;
using Core.DataFilters;
using Core.Entities;
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
            var inmatesSpec = new InmateWithLeaveSpecification();
            var inmates = await  _unitOfWork.Repository<Inmate>().FindAllBySpecAsync(inmatesSpec);

            var monthlyBillDetails = await GenerateMonthlyBillAsync(month, year);

            var firstDayOfMonth = new DateTime(year, month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            foreach (var inmate in inmates)
            {
                var leavesForInmate = inmate.InmateLeaves;
                var numberOfDays = 0;
                if (leavesForInmate.Any())
                {
                    var leaveDaysInMonth = leavesForInmate.Where(l =>
                        (l.FromDate >= firstDayOfMonth) || (l.ToDate <= lastDayOfMonth));

                    foreach (var leave in leaveDaysInMonth)
                    {
                        if (leave.ToDate < lastDayOfMonth)
                           numberOfDays = (leave.ToDate - leave.FromDate).Days;
                        if (leave.FromDate)
                    }
                }
            }
            
            
            
            
        }
    }
}