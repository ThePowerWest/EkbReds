using ApplicationCore.Entities.Identity;
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
        /// Список матчей для отображения на главной странице
        /// </summary>
        /// <returns>Матчи</returns>
        Task<IEnumerable<Match>> IndexList(User currentUser);
    }
}