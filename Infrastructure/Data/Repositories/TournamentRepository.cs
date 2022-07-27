using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Репозиторий сущности Турнир
    /// </summary>
    public class TournamentRepository : EFRepository<Tournament>, IReadRepository<Tournament>, IRepository<Tournament>, ITournamentRepository
    {
        /// <summary>
        /// ctor
        /// </summary>
        public TournamentRepository(MainContext context) : base(context)
        { }

        /// <inheritdoc />
        public IEnumerable<Tournament> Currents() => 
            Context.Tournaments.Where(tournament => 
                tournament.Season == Context.Seasons.OrderByDescending(season => season.YearStart)).AsNoTracking();
    }
}