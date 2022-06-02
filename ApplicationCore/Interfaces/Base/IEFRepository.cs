using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Base
{
    /// <summary>
    /// EF repository
    /// </summary>
    /// <typeparam name="T">Universal parameter</typeparam>
    public interface IEFRepository<T>
    {
        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Entity</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="paramsToAttach">Object to add with entity</param>
        /// <returns>Entity</returns>
        Task<T> AddAsync(T entity, params object[] paramsToAttach);

        /// <summary>
        /// Add list of entities
        /// </summary>
        /// <param name="entity">List of entities</param>
        /// <returns>List of entities</returns>
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity);

        /// <summary>
        /// Get list entity
        /// </summary>
        /// <returns>List</returns>
        Task<IEnumerable<T>> ListAsync();

        /// <summary>
        /// Add entity or update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="id">Entity id</param>
        /// <returns>Entity</returns>
        Task<T> AddOrUpdateAsync(T entity, int id = 0);

        /// <summary>
        /// Find entity
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        Task<T> FindAsync(int id);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="id">Identifier</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        Task UpdateAsync(T entity);
    }
}