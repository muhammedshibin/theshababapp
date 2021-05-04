using Core.DataFilters;
using Core.Entities;

namespace Core.Specifications
{
    public class InmateSpecification : BaseSpecification<Inmate>
    {
        public InmateSpecification(InmateFilter filter, bool forCount = false) : 
            base(i => (string.IsNullOrEmpty(filter.Search)|| i.FullName.ToLower().Contains(filter.Search)))
        {
            AddOrderBy(i => i.FullName);

            if (string.IsNullOrEmpty(filter.Sort))
            {
                switch (filter.Sort)
                {
                    case "byName":
                        AddOrderBy(i => i.FullName);
                        break;
                    case "byNameDesc":
                        AddOrderByDesc(i => i.FullName);
                        break;
                }   
            }
            if(!forCount) AddPagination((filter.PageIndex - 1) * filter.PageSize , filter.PageSize);
        }
        
    }
}