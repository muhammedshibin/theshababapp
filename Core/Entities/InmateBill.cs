using Core.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class InmateBill : BaseEntity
    {
        [Required]
        public int Month { get; set; }
        [Required]
        public int Year { get; set; }
        public decimal BillAmount { get; set; }
        public Inmate Inmate { get; set; }
        public int InmateId { get; set; }
        public int? PaymentId { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.NotPaid;
        public BillPayment BillPayment { get; set; }
        public List<BillDetail> BillItems { get; set; }
    }
}
