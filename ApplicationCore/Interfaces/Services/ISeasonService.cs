namespace ApplicationCore.Interfaces.Services
{
    /// <summary>
    /// Интерфейс сервиса Сезонов
    /// </summary>
    public interface ISeasonService
    {
        /// <summary>
        /// Создать новый сезон если он есть
        /// </summary>
        Task CreateAsync();
    }
}