using ApplicationCore.Entities.Main;

namespace ApplicationCore.Models
{
    /// <summary>
    /// Предыдущий матч
    /// </summary>
    public class PreviousMatch
    {
        /// <summary>
        /// Название домашней команды
        /// </summary>
        public string HomeTeamName { get; set; } = string.Empty;

        /// <summary>
        /// Количество голов, которые забила домашняя команда
        /// </summary>
        public byte HomeTeamScore { get; set; }

        /// <summary>
        /// Название выездной команды
        /// </summary>
        public string AwayTeamName { get; set; } = string.Empty;

        /// <summary>
        /// Количество голов, которые забила выездная команда
        /// </summary>
        public byte AwayTeamScore { get; set; }

        /// <summary>
        /// Прогноз на матч
        /// </summary>
        public Prediction? Prediction { get; set; }
    }
}