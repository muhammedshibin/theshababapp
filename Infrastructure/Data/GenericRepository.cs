using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ShababContext _context;
        private readonly IMapper _mapper;

        public GenericRepository(ShababContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<T> FindByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<IReadOnlyList<T>> FindAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> FindOneBySpecAsync(ISpecification<T> spec)
        {
            return await EvaluateExpression(spec).FirstOrDefaultAsync();
        }
        public async Task<TOut> FindOneBySpecAsync<TOut>(ISpecification<T> spec)
        {
            return await EvaluateExpression(spec)
                .ProjectTo<TOut>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<T>> FindAllBySpecAsync(ISpecification<T> spec)
        {
            return await EvaluateExpression(spec).ToListAsync();
        }
        public async Task<IReadOnlyList<TOut>> FindAllBySpecAsync<TOut>(ISpecification<T> spec,Expression<Func<T,TOut>> select)
        {
            return await EvaluateExpression(spec).Select(select).ToListAsync();
        }
        public async Task<IReadOnlyList<TOut>> FindAllBySpecAsync<TOut>(ISpecification<T> spec)
        {
            return await EvaluateExpression(spec)
                .ProjectTo<TOut>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
        public T Add(T tEntity)
        {
            tEntity.CreatedOn = DateTime.Now;
            _context.Set<T>().Add(tEntity);
            return tEntity;
        }

        public void Update(T tEntity)
        {
            _context.Set<T>().Attach(tEntity);
            _context.Entry(tEntity).State = EntityState.Modified;
           
        }

        public void AddMany(IEnumerable<T> tEntities)
        {
            foreach (var baseEntity in tEntities)
            {
                baseEntity.CreatedOn = DateTime.Now;
            }
            _context.Set<T>().AddRange(tEntities);
        }
        
        public void RemoveMany(IEnumerable<T> tEntities)
        {
            _context.Set<T>().RemoveRange(tEntities);
        }
        
        public void Remove(T tEntity)
        {
            _context.Set<T>().Remove(tEntity);
        }

        private IQueryable<T> EvaluateExpression(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GenerateExpression(_context.Set<T>(), spec);
        }

        public async Task<int> GetCountForSpecAsync(ISpecification<T> spec)
        {
            return await EvaluateExpression(spec).CountAsync();
        }
    }
}