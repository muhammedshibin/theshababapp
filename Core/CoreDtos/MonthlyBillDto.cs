using System.Collections.Generic;

namespace Core.CoreDtos
{
    public class MonthlyBillDto
    {
        public MonthlyBillDto(int month,int year)
        {
            Month = month;
            Year = year;
        }
        public int Month { get; set; }
        public int Year { get; set; }
        
        public double SubTotal { get; set; }
        public List<CategoryWiseExpense> CategoryWiseExpenses { get; set; }
    }

    public class CategoryWiseExpense
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<TransactionDetailDto> TransactionDetails { get; set; }
        public double TotalAmount { get; set; } = 0;
    }

    public class TransactionDetailDto
    {
        public string TransactionDetailName { get; set; }
        public double  Amount { get; set; }
    }
}