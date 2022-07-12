using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Репозиторий прогнозов
    /// </summary>
    public class PredictionRepository : EfRepository<Prediction>, IReadRepository<Prediction>, IRepository<Prediction>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public PredictionRepository(MainContext context) : base(context)
        { }
    }
}