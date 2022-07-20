using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Main;
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

        /// <summary>
        /// Коллекция токенов в БД
        /// </summary>
        public DbSet<SportScoreToken> SportScoreTokens { get; set; }

        /// <summary>
        /// Коллекция сезонов в БД
        /// </summary>
        //public DbSet<Season> Seasons { get; set; }

        ///// <summary>
        ///// Коллекция матчей в БД
        ///// </summary>
        //public DbSet<Match> Matches { get; set; }

        ///// <summary>
        ///// Коллекция лиг в БД
        ///// </summary>
        //public DbSet<League> Leagues { get; set; }

        ///// <summary>
        ///// Коллекция прогнозов в БД
        ///// </summary>
        //public DbSet<Prediction> Predictions { get; set; }
    }
}