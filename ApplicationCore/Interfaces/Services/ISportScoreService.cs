using ApplicationCore.Models.SportScore.Teams;

namespace ApplicationCore.Interfaces.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с API SportScore
    /// </summary>
    public interface ISportScoreService
    {
        /// <summary>
        /// Получить текущий сезон из SportScore
        /// </summary>
        /// <returns>Текущий сезон</returns>
        Task<Tournament> CurrentSeasonAsync();

        /// <summary>
        /// Получить все матчи с 1й страницы
        /// </summary>
        /// <returns>Список матчей</returns>
        Task<IEnumerable<EventData>> AllMatchesAsync();

        /// <summary>
        /// Получить список всех турниров за этот сезон
        /// </summary>
        /// <param name="yearEnd">Год окончания сезона</param>
        /// <returns>Список турниров</returns>
        Task<IEnumerable<Tournament>> GetTournamentsCurrentSeasonAsync(int yearEnd);
    }
}