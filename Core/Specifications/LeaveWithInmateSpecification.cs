using Core.DataFilters;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class LeaveWithInmateSpecification : BaseSpecification<Leave>
    {
        public LeaveWithInmateSpecification(LeavesFilter filter) : base(x =>
        (!filter.LeaveId.HasValue || x.Id == filter.LeaveId) &&
        (!filter.InmateId.HasValue || x.InmateId == filter.InmateId)
        )
        {
            AddInclude(x => x.Inmate);
        }
    }
}
