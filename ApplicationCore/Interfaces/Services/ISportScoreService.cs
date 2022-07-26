namespace ApplicationCore.Interfaces.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с API SportScore
    /// </summary>
    public interface ISportScoreService
    {
        /// <summary>
        /// Проверить какой сейчас сезон и при необходимости обновить
        /// </summary>
        Task UpdateSeason();

        /// <summary>
        /// Проверить список турниров и при необходимости обновить
        /// </summary>
        Task UpdateTournaments();

        /// <summary>
        /// Проверить сколько матчей осталось в списке и при необходимости обновить
        /// </summary>
        Task UpdateMatches();
    }
}