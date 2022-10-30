using ApplicationCore.Entities.Main;

namespace ApplicationCore.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс репозитория сущности Сезон
    /// </summary>
    public interface ISeasonRepository
    {
        /// <summary>
        /// Получить последний (текущий) сезон
        /// </summary>
        /// <returns>Текущий сезон</returns>
        Task<Season> CurrentAsync();

        /// <summary>
        /// Получить текущий сезон
        /// включая турниры
        /// </summary>
        /// <returns>Текущий сезон</returns>
        Task<Season> CurrentIncTAsync();

        /// <summary>
        /// Получить названия месяцев в сезоне
        /// </summary>
        /// <param name="seasonId">Идентификатор сезона</param>
        /// <returns>Наименование месяцев</returns>
        Task<IEnumerable<string>> GetMonthsAsync(int seasonId);
    }
}