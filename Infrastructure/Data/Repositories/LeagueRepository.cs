using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Репозиторий лиг
    /// </summary>
    public class LeagueRepository : EfRepository<League>, IReadRepository<League>, IRepository<League>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public LeagueRepository(MainContext context) : base(context)
        { }
    }
}