using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BillPayment : BaseEntity
    {
        public int InmateId { get; set; }
        public Inmate Inmate { get; set; }
        public DateTime PaidOn { get; set; }
        public decimal Amount { get; set; }
        public InmateBill Bill { get; set; }
        public int? BillId { get; set; }
    }
}
