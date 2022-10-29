using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Репозиторий сущности Пользователь
    /// </summary>
    public class UserRepository : EFRepository<User>, IReadRepository<User>, IRepository<User>, IUserRepository
    {
        /// <summary>
        /// ctor
        /// </summary>
        public UserRepository(MainContext context) : base(context)
        { }

        /// <inheritdoc />
        public async Task<IEnumerable<User>> RandomUsersAsync(byte number) =>
            await Context.Users.FromSqlRaw(
                @$"SELECT TOP {number} * 
                   FROM AspNetUsers
                   WHERE EmailConfirmed = 1
                   ORDER BY NEWID()")
               .AsNoTracking()
               .ToListAsync();
    }
}