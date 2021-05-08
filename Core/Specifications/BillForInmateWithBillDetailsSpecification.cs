using Core.DataFilters;
using Core.Entities;
using Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BillForInmateWithBillDetailsSpecification : BaseSpecification<InmateBill>
    {
        public BillForInmateWithBillDetailsSpecification(int inmateId) : base(b => b.InmateId == inmateId)
        {
            AddInclude(b => b.BillItems);
            AddInclude(b => b.Inmate);
        }

        public BillForInmateWithBillDetailsSpecification(BillFilter filter, bool forCount = false) :
            base(b =>
            (!filter.Month.HasValue || b.Month == filter.Month) &&
            (!filter.Year.HasValue || b.Year == filter.Year) &&
            (string.IsNullOrEmpty(filter.PaidPredicate) || b.PaymentStatus == (PaymentStatus) Enum.Parse(typeof(PaymentStatus),filter.PaidPredicate)) &&
            (string.IsNullOrEmpty(filter.Search) || b.Inmate.FullName.ToLower().Contains(filter.Search)))
        {
            if (!forCount)
            {
                //AddInclude(b => b.BillItems);
                AddInclude(b => b.Inmate);
                AddOrderByDesc(b => b.CreatedOn);
                AddPagination((filter.PageIndex - 1) * filter.PageSize, filter.PageSize);
            }           
        }
    }
}
