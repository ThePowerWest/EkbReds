using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class TournamentRepository : EFRepository<Tournament>, IReadRepository<Tournament>, IRepository<Tournament>, ITournamentRepository
    {
        /// <summary>
        /// ctor
        /// </summary>
        public TournamentRepository(MainContext context) : base(context)
        { }

        public async Task<Tournament> FindByNameAsync(string name)
        {
            return await Context.Tournaments.Where(t => t.Name == name)
                .FirstAsync();
        }
    }
}