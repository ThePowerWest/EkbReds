using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Main;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /// <summary>
    /// Основной контекст приложения
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
            builder.Entity<User>().Property(user => user.Id).ValueGeneratedOnAdd();
            builder.Entity<Role>().Property(role => role.Id).ValueGeneratedOnAdd();

            builder.Entity<User>().HasMany(user => user.Predictions).WithOne(prediction => prediction.User).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }

        public DbSet<Season> Seasons { get; set; }

        public DbSet<Tournament> Tournaments { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Prediction> Predictions { get; set; }

        public DbSet<SportScoreToken> SportScoreTokens { get; set; }
    }
}