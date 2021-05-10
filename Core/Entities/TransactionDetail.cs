using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enumerations;

namespace Core.Entities
{
    public class TransactionDetail : BaseEntity
    {
        public string Name { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsExpense { get; set; }
        public int PaidPartyId { get; set; }
        public Vendor PaidParty { get; set; }
        public int PaidToId { get; set; }
        public Vendor PaidTo { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public TransactionStatus Status { get; set; } = TransactionStatus.Saved;
    }
}
