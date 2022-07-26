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
        Task<Season> LastAsync();
    }
}