using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Репозиторий сезонов
    /// </summary>
    public class SeasonRepository : EfRepository<Season>, IReadRepository<Season>, IRepository<Season>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public SeasonRepository(MainContext context) : base(context)
        { }
    }
}