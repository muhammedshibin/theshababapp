using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T,bool>> Criteria { get;}
        public List<Expression<Func<T,object>>> Includes { get;}
        public Expression<Func<T,object>> OrderBy { get; }
        public Expression<Func<T,object>> OrderByDesc { get; }
        public int Skip { get;}
        public int Take { get;}
        public bool IsPagingEnabled { get; }
        
    }
}