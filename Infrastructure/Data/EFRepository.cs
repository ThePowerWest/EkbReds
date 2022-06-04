using Ardalis.Specification.EntityFrameworkCore;
using ApplicationCore.Interfaces;

namespace Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class
{
    public EfRepository(MainContext dbContext) : base(dbContext)
    {
    }
}
