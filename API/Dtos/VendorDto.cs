using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class VendorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double AmountInHand { get; set; }
        public double DueAmount { get; set; }
    }
}
