using Core.Entities;

namespace Core.Specifications
{
    public class InmateWithLeaveSpecification : BaseSpecification<Inmate>
    {
        public InmateWithLeaveSpecification()
        {
            AddInclude(i => i.InmateLeaves);
        }
    }
}