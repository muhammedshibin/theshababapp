using System;
using System.Collections;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly ShababContext _context;

        public UnitOfWork(ShababContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            var entityName = typeof(T).Name;
            _repositories ??= new Hashtable();

            if (_repositories.ContainsKey(entityName)) return (IGenericRepository<T>) _repositories[entityName];
            var repositoryInstance =
                Activator.CreateInstance(typeof(GenericRepository<>).MakeGenericType(), _context);
            _repositories.Add(entityName,repositoryInstance);
            return (IGenericRepository<T>)_repositories[entityName];
        }

        public async Task<int> Complete()
        {
           return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}