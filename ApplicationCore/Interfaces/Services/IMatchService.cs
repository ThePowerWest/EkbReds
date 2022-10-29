namespace ApplicationCore.Interfaces.Services
{
    /// <summary>
    /// Сервис для работы с матчами
    /// </summary>
    public interface IMatchService
    {
        /// <summary>
        /// Создать или обновить матчи
        /// </summary>
        Task CreateOrUpdateAsync();
    }
}