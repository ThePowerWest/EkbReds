using ApplicationCore.Models;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для загрузки матчей
    /// </summary>
    public interface IMatchLoadService
    {
        /// <summary>
        /// Загрузить матчи
        /// </summary>
        Task<List<Match>> LoadAsync();
    }
}