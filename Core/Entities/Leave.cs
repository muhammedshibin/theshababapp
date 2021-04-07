using System;
using Core.Enumerations;

namespace Core.Entities
{
    public class Leave : BaseEntity
    {
        public string LeaveReason { get; set; }
        public DateTime FromDate { get; set; } = DateTime.Now;
        public DateTime ToDate { get; set; }
        public Inmate Inmate { get; set; }
        public int InmateId { get; set; }
        public LeaveStatus Status { get; set; } = LeaveStatus.Applied;

    }
}