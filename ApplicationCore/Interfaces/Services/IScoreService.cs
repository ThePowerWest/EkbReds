namespace ApplicationCore.Interfaces.Services
{
    /// <summary>
    /// Cервиса подсчёта очков за проставленные прогнозы
    /// </summary>
    public interface IScoreService
    {
        /// <summary>
        /// Пересчитать очки
        /// </summary>
        Task RecalculatePoints();
    }
}