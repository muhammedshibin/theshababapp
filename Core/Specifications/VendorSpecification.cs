using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class VendorSpecification : BaseSpecification<Vendor>
    {
       
        public VendorSpecification(List<int> vendorIds) : base(v => vendorIds.Contains(v.Id)) { }
        public VendorSpecification(List<string> vendorNames) : base(v => vendorNames.Contains(v.Name)) { }
       
    }
}
