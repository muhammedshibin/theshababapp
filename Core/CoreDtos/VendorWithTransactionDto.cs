using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CoreDtos
{
    public class VendorTransactionDto
    {
        public string Name { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsExpense { get; set; }
        public string PaidTo { get; set; }
        public string PaidBy { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
    }
}
