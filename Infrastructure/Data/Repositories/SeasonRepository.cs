using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class SeasonRepository : EFRepository<Season>, IReadRepository<Season>, IRepository<Season>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public SeasonRepository(MainContext context) : base(context)
        { }

        public async Task<Season> LastAsync()
        {
            return await Context.Seasons
                             .OrderByDescending(season => season.Id)
                             .FirstOrDefaultAsync();
        }
    }
}