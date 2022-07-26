using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Репозиторий сущности Прогноз
    /// </summary>
    public class PredictionRepository : EFRepository<Prediction>, IReadRepository<Prediction>, IRepository<Prediction>, IPredictionRepository
    {
        /// <summary>
        /// ctor
        /// </summary>
        public PredictionRepository(MainContext context) : base(context)
        { }

        /// <inheritdoc />
        public async Task<Prediction?> FirstOrDefaultAsync(Match match, User user)
            => await Context.Predictions.AsNoTracking()
                      .FirstOrDefaultAsync(prediction => prediction.Match == match &&
                                                         prediction.User == user);


    }
}