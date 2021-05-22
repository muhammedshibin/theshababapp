using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class VendorDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public double AmountInHand { get; set; }
        public double DueAmount { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
