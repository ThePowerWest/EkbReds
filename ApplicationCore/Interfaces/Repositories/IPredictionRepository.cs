using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Main;

namespace ApplicationCore.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс репозитория сущности Прогноз
    /// </summary>
    public interface IPredictionRepository
    {
        /// <summary>
        /// Поиск прогноза
        /// </summary>
        /// <param name="match">Матч в котором необходимо найти прогноз</param>
        /// <param name="user">Польщователь, который проставил данный прогноз</param>
        /// <returns>Прогноз или пустое значение если не найдет</returns>
        Task<Prediction?> FirstOrDefaultAsync(Match match, User user);
    }
}