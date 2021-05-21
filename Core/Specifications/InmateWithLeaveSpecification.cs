using Core.Entities;
using Core.Enumerations;

namespace Core.Specifications
{
    public class InmateWithLeaveSpecification : BaseSpecification<Inmate>
    {
        public InmateWithLeaveSpecification(): base(inmate => inmate.Status == InmateStatus.Active)
        {
            AddInclude(i => i.InmateLeaves);
        }
    }
}