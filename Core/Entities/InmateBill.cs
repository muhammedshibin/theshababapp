using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class InmateBill : BaseEntity
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public double BillAmount { get; set; }
        public Inmate Inmate { get; set; }
        public int InmateId { get; set; }
        public List<BillDetail> BillItems { get; set; }
    }
}
