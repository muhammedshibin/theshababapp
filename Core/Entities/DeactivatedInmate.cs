using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class DeactivatedInmate : BaseEntity
    {
        public string Reason { get; set; }
        public int InmateId { get; set; }
        public Inmate Inmate { get; set; }
        public bool IsSettlement { get; set; }
        public decimal Amount { get; set; }
        public DateTime LastDate { get; set; }
    }
}
