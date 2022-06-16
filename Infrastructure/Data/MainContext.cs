using ApplicationCore.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /// <summary>
    /// Контекст БД
    /// </summary>
    public class MainContext : IdentityDbContext<User, Role, string>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public MainContext(DbContextOptions<MainContext> options) : base(options) { }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Entity<Role>().Property(e => e.Id).ValueGeneratedOnAdd();

            base.OnModelCreating(builder);
        }
    }
}