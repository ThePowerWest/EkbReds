using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Репозиторий сущности Матч
    /// </summary>
    public class MatchRepository : EFRepository<Match>, IReadRepository<Match>, IRepository<Match>, IMatchRepository
    {
        /// <summary>
        /// ctor
        /// </summary>
        public MatchRepository(MainContext context) : base(context)
        { 
        
        }

        /// <inheritdoc />
        public IEnumerable<Match> Currents() =>
            Context.Matches
                .Include(match => match.Tournament)
                .Where(match =>
                       match.Tournament.Season == Context.Seasons.OrderByDescending(season => season.YearStart)
                                                                 .First());

        /// <inheritdoc />
        public IEnumerable<Match> Next(int count) => 
            Context.Matches
                .Include(match => match.Tournament)
                .OrderBy(match => match.StartDate)
                    .AsNoTracking()
                .Where(match => DateTime.Now < match.StartDate)
                .Take(count);
    }
}