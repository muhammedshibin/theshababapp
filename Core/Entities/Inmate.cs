using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enumerations;

namespace Core.Entities
{
    public class Inmate : BaseEntity
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Address { get; set; }        
        public string EmailAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string PictureUrl { get; set; }
        public InmateStatus Status { get; set; } = InmateStatus.Active;
        public bool IsVisit { get; set; } = false;
        public bool IsInmateOnTopBed { get; set; } = false;


        public IReadOnlyList<Leave> InmateLeaves;
    }
}
