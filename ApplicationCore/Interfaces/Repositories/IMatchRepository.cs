using ApplicationCore.Entities.Main;

namespace ApplicationCore.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс репозитория сущности Матч
    /// </summary>
    public interface IMatchRepository
    {
        /// <summary>
        /// Список матчей за этот сезон
        /// </summary>
        ///<returns>Матчи</returns>
        IEnumerable<Match> Currents();

        /// <summary>
        /// Получить следующие матчи
        /// </summary>
        /// <returns>Список следующих матчей</returns>
        IEnumerable<Match> Next(int count);
    }
}