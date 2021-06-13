using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class DeactivateInmateDto
    {
        public int InmateId { get; set; }
        public string Reason { get; set; }
        public bool IsSettlement { get; set; }
        public decimal Amount { get; set; }
        public DateTime LastDate { get; set; }
    }
}
