using Core.Entities;
using Core.Enumerations;
using System.Linq;

namespace Core.Specifications
{
    public class InmateWithLeaveSpecification : BaseSpecification<Inmate>
    {
        public InmateWithLeaveSpecification(): base(inmate => inmate.Status == InmateStatus.Active || inmate.Status == InmateStatus.Leaving)
        {
            AddInclude(i => i.InmateLeaves.Where(x => x.Status == LeaveStatus.Applied || x.Status == LeaveStatus.Approved));
        }
    }
}