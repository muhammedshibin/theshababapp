using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Vendor : BaseEntity
    {
        public string Name { get; set; }
        public decimal AmountInHand { get; set; }
        public double DueAmount { get; set; }
        public List<TransactionDetail> IncomingTransactions { get; set; }
        public List<TransactionDetail> OutgoingTransactions { get; set; }

    }
}
