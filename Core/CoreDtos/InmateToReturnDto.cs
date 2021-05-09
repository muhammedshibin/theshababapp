using Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.CoreDtos
{
    public class InmateToReturnDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }      
        public string PhoneNumber { get; set; }
        public string PictureUrl { get; set; }
        public InmateStatus Status { get; set; }
    }
}
