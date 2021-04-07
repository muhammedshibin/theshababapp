using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Find an Entity By It's Primary Key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> FindByIdAsync(int id);
        /// <summary>
        /// Retrieve all items from database of a Particular Entity
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyList<T>> FindAllAsync();
        /// <summary>
        /// Retrieve an entity item for the provided input specification
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        Task<T> FindOneBySpecAsync(ISpecification<T> spec);
        /// <summary>
        /// Retrieve list of items of entity for the provided specification
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> FindAllBySpecAsync(ISpecification<T> spec);
        /// <summary>
        /// Add an Item to the database , Executed only when SaveChanges method is called
        /// </summary>
        /// <param name="tEntity"></param>
        void Add(T tEntity);
        /// <summary>
        /// Deletes an Item from Database , Executed only when SaveChanges method is called
        /// </summary>
        /// <param name="tEntity"></param>
        void Remove(T tEntity);
        /// <summary>
        /// Add List of Items to Database
        /// </summary>
        /// <param name="tEntities"></param>
        void AddMany(IEnumerable<T> tEntities);
        /// <summary>
        /// Remove a list of Items from Database
        /// </summary>
        /// <param name="tEntities"></param>
        void RemoveMany(IEnumerable<T> tEntities);
    }
}