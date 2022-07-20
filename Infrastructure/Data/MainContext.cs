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
            builder.Entity<User>().Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Entity<Role>().Property(e => e.Id).ValueGeneratedOnAdd();

            base.OnModelCreating(builder);
        }

        public DbSet<Season> Seasons { get; set; }

        public DbSet<Tournament> Tournaments { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Prediction> Predictions { get; set; }

        public DbSet<SportScoreToken> SportScoreTokens { get; set; }
    }
}