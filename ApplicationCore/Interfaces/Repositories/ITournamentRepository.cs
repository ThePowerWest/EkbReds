using ApplicationCore.Entities.Main;

namespace ApplicationCore.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс репозитория сущности Турнир
    /// </summary>
    public interface ITournamentRepository
    {
        /// <summary>
        /// Список турниров за этот сезон
        /// </summary>
        ///<returns>Турниры</returns>
        IEnumerable<Tournament> Currents();
    }
}
