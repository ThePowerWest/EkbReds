namespace ApplicationCore.Interfaces.Services
{
    /// <summary>
    /// Сервис для работы с турнирами
    /// </summary>
    public interface ITournamentService
    {
        /// <summary>
        /// Добавить новый турнир
        /// </summary>
        Task CreateAsync();
    }
}