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
        public async Task<Season> LastAsync()
        {
            return await Context.Seasons
                .Include(season => season.Tournaments)
                    .ThenInclude(tournament => tournament.Matches)
                .OrderByDescending(season => season.Id)
                .FirstAsync();
        }
    }
}