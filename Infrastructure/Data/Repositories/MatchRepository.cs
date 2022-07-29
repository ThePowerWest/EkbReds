using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Репозиторий сущности Матч
    /// </summary>
    public class MatchRepository : EFRepository<Match>, IReadRepository<Match>, IRepository<Match>, IMatchRepository
    {
        private readonly UserManager<User> UserManager;

        /// <summary>
        /// ctor
        /// </summary>
        public MatchRepository(
            MainContext context,
            UserManager<User> userManager) : base(context)
        {
            UserManager = userManager;
        }

        /// <inheritdoc />
        public IEnumerable<Match> Currents() =>
            Context.Matches
                .Include(match => match.Tournament)
                .Where(match =>
                       match.Tournament.Season == Context.Seasons.OrderByDescending(season => season.YearStart)
                                                                 .First());

        /// <inheritdoc />
        public async Task<IEnumerable<Match>> IndexList(User user)
        {
            // TODO переделать на один запрос с вложенным подзапросом
            List<Match> beforeNextMatches =
                await Context.Matches
                     .Include(match => match.Predictions)
                        .ThenInclude(prediction => prediction.User)
                     .Include(match => match.Tournament)
                     .Where(match =>
                            match.Tournament.Season == Context.Seasons.OrderByDescending(season => season.YearStart).First() &&
                            DateTime.Now < match.StartDate)
                     .OrderBy(match => match.StartDate)
                     .Take(4)
                     .ToListAsync();

            List<Match> pastMatches =
                await Context.Matches
                     .Include(match => match.Predictions)
                     .Where(match =>
                            match.Tournament.Season == Context.Seasons.OrderByDescending(season => season.YearStart).First() &&
                            DateTime.Now > match.StartDate)
                     .OrderByDescending(match => match.StartDate)
                     .Take(3)
                     .ToListAsync();

            pastMatches.AddRange(beforeNextMatches);

            foreach (Match match in pastMatches)
            {
                match.Predictions = match.Predictions.Where(prediction => prediction.User == user).ToList();
            }

            return pastMatches;
        }

        /// <inheritdoc />
        public IEnumerable<Match> Next(int count) => 
            Context.Matches
                .Include(match => match.Tournament)
                    .ThenInclude(tournament => tournament.Season)
                .OrderBy(match => match.StartDate)
                    .AsNoTracking()
                .Where(match => DateTime.Now < match.StartDate)
                .Take(count);
    }
}