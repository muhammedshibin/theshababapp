using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enumerations;

namespace Core.Entities
{
    public class Inmate : BaseEntity
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string PictureUrl { get; set; }
        public InmateStatus Status { get; set; } = InmateStatus.Active;
        public bool IsVisit { get; set; } = false;
        public bool IsInmateOnTopBed { get; set; } = false;


        public IReadOnlyList<Leave> InmateLeaves;
    }
}
