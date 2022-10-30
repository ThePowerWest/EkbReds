using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Main;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс репозитория сущности Матч
    /// </summary>
    public interface IMatchRepository
    {
        /// <summary>
        /// Получить все не начавшиеся матчи
        /// </summary>
        /// <returns>Список матчей</returns>
        Task<IEnumerable<Match>> GetNotStartedAsync();

        /// <summary>
        /// Список прошедших матчей
        /// </summary>
        /// <returns>Матчи</returns>
        Task<IEnumerable<Match>> PastAsync();

        /// <summary>
        /// Список матчей за этот сезон
        /// </summary>
        ///<returns>Матчи</returns>
        Task<IEnumerable<Match>> CurrentsAsync();

        /// <summary>
        /// Следующий матч
        /// </summary>
        /// <param name="user">Пользователь для которого необходимо загрузить следующий матч</param>
        /// <returns>Матч</returns>
        Task<NextMatch?> NextAsync(User user);

        /// <summary>
        /// Следующий матч
        /// </summary>
        /// <returns>Матч</returns>
        Task<NextMatch?> NextAsync();

        /// <summary>
        /// Список прошедших матчей
        /// </summary>
        /// <param name="number">Количество предыдущих матчей которые необходимо загрузить</param>
        /// <param name="userId">Идентификатор пользователья по которому будет добавлены прогнозы на эти матч</param>
        /// <returns>Список матчей</returns>
        Task<IEnumerable<PreviousMatch>> PreviousAsync(byte number, string userId);

        /// <summary>
        /// Список прошедших матчей
        /// </summary>
        /// <param name="number">Количество предыдущих матчей которые необходимо загрузить</param>
        /// <returns>Список матчей</returns>
        Task<IEnumerable<PreviousMatch>> PreviousAsync(byte number);

        /// <summary>
        /// Список следующий матчей исключая ближайший
        /// </summary>
        /// <param name="number">Количество следующих матчей которые необходимо загрузить</param>
        /// <returns>Список матчей</returns>
        Task<IEnumerable<FutureMatch>> FutureAsync(byte number);
    }
}