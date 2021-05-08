using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CoreDtos
{
    public class PaymentDto
    {
        public int InmateId { get; set; }
        public DateTime? PaidOn { get; set; }
        public double Amount { get; set; }
        public int? BillId { get; set; }
    }
}
