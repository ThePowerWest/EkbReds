using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Репозиторий сущности Сезон
    /// </summary>
    public class SeasonRepository : EFRepository<Season>, IReadRepository<Season>, IRepository<Season>, ISeasonRepository
    {
        /// <summary>
        /// ctor
        /// </summary>
        public SeasonRepository(MainContext context) : base(context)
        { }

        /// <inheritdoc />
        public async Task<Season> CurrentAsync() =>
            await Context.Seasons
                .OrderByDescending(season => season.Id)
                .AsNoTracking()
                .FirstAsync();

        /// <inheritdoc />
        public async Task<Season> CurrentIncTAsync() =>
            await Context.Seasons
                .Include(season => season.Tournaments)
                    .OrderByDescending(season => season.Id)
                    .FirstAsync();

        /// <inheritdoc />
        public async Task<IEnumerable<string>> GetMonthsAsync(int seasonId)
        {
            List<string> months = await Context.Matches
                .Where(match => match.Tournament.Season.Id == seasonId)
                .OrderBy(match => match.StartDate)
                .Select(match => match.StartDate.ToString("MMMM"))
                .AsNoTracking()
                .ToListAsync();

            return months.Distinct();
        }
    }
}