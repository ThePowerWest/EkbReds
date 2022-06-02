using ApplicationCore.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    /// <summary>
    /// ЕF repository
    /// </summary>
    /// <typeparam name="T">Parameter</typeparam>
    public class EFRepository<T> : IEFRepository<T> where T : class
    {
        protected readonly MainContext _dbMainContext;

        /// <summary>
        /// ctor
        /// </summary>
        public EFRepository(MainContext dbMainContext) => _dbMainContext = dbMainContext;

        /// <inheritdoc />
        public async Task<T> AddAsync(T entity)
        {
            await _dbMainContext.Set<T>().AddAsync(entity);
            await _dbMainContext.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc />
        public async Task<T> AddAsync(T entity, params object[] paramsToAttach)
        {
            _dbMainContext.AttachRange(paramsToAttach);

            await _dbMainContext.Set<T>().AddAsync(entity);
            await _dbMainContext.SaveChangesAsync();

            return entity;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity)
        {
            await _dbMainContext.Set<T>().AddRangeAsync(entity);
            await _dbMainContext.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc />
        public async Task<T> AddOrUpdateAsync(T entity, int id = 0)
        {
            T item = _dbMainContext.Set<T>().Find(id);
            if (item != null) _dbMainContext.Update(entity);
            else await _dbMainContext.Set<T>().AddAsync(entity);

            await _dbMainContext.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<T>> ListAsync() => await _dbMainContext.Set<T>().ToListAsync();

        /// <inheritdoc />
        public async Task<T> FindAsync(int id) => await _dbMainContext.Set<T>().FindAsync(id);

        /// <inheritdoc />
        public async Task DeleteAsync(int id)
        {
            T entity = await _dbMainContext.Set<T>().FindAsync(id);
            _dbMainContext.Set<T>().Remove(entity);
            await _dbMainContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateAsync(T entity)
        {
            _dbMainContext.Entry(entity).State = EntityState.Modified;
            await _dbMainContext.SaveChangesAsync();
        }
    }
}