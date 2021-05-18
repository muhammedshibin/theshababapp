using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class LeaveDto
    {
        public int Id { get; set; }
        public string LeaveReason { get; set; }
        public DateTime FromDate { get; set; } = DateTime.Now;
        public DateTime ToDate { get; set; }
        public string Inmate { get; set; }
        public int InmateId { get; set; }
        public string Status { get; set; }
    }
}
