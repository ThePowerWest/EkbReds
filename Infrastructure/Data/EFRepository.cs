using Ardalis.Specification.EntityFrameworkCore;
using ApplicationCore.Interfaces;

namespace Infrastructure.Data;
    /// <summary>
    /// Репозиторий. Через конструктор добавлен контекст БД.
    /// </summary>
public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class
{
    public EfRepository(MainContext dbContext) : base(dbContext)
    {
    }
}