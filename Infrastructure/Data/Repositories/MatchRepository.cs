using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Репозиторий матчей
    /// </summary>
    public class MatchRepository : EfRepository<Match>, IReadRepository<Match>, IRepository<Match>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public MatchRepository(MainContext context) : base(context)
        { }
    }
}