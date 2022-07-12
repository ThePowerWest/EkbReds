using ApplicationCore.Models;

namespace ApplicationCore.Interfaces.SportScore
{
    /// <summary>
    /// Интерфейс сервиса для загрузки матчей
    /// </summary>
    public interface IMatchLoadService
    {
        /// <summary>
        /// Загрузить матчи
        /// </summary>
        Task<Match> LoadAsync();
    }
}