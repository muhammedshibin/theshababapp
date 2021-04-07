using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Vendor : BaseEntity
    {
        public string Name { get; set; }
        public double AmountInHand { get; set; }
        public double DueAmount { get; set; }
        
    }
}
