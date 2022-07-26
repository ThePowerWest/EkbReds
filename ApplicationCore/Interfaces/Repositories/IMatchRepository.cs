using ApplicationCore.Entities.Main;

namespace ApplicationCore.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс репозитория сущности Матч
    /// </summary>
    public interface IMatchRepository
    {
        /// <summary>
        /// Получить следующие матчи
        /// </summary>
        /// <returns>Список следующих матчей</returns>
        IEnumerable<Match> Next(int count);
    }
}