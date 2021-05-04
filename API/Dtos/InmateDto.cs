using Core.Enumerations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class InmateDto
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Address { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string PictureUrl { get; set; }
        public IFormFile InmatePhoto { get; set; }
        public InmateStatus Status { get; set; } = InmateStatus.Active;
        public bool IsVisit { get; set; } = false;
        public bool IsInmateOnTopBed { get; set; } = false;
    }
}
