using System.Linq;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GenerateExpression(IQueryable<T> inputQuery,ISpecification<T> spec)
        {
            var query = inputQuery.AsQueryable();

            if (!spec.TrackingNeeded)
            {
                query = query.AsNoTracking();
            }
            
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            
            if (spec.OrderByDesc != null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.Includes.Any())
            {
                query = spec.Includes.Aggregate(query, (current, next) => current.Include((next)));
            }

            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            return query;

        }
        
    }
}