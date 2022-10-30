using ApplicationCore.Entities.Main;

namespace ApplicationCore.Models
{
    /// <summary>
    /// Будущий матч
    /// </summary>
    public class FutureMatch
    {
        /// <summary>
        /// Название домашней команды
        /// </summary>
        public string HomeTeamName { get; set; } = string.Empty;

        /// <summary>
        /// Ссылка на эмблему домашней команды
        /// </summary>
        public string HomeTeamLogo { get; set; } = string.Empty;

        /// <summary>
        /// Название выездной команды
        /// </summary>
        public string AwayTeamName { get; set; } = string.Empty;

        /// <summary>
        /// Ссылка на эмблему выездной команды
        /// </summary>
        public string AwayTeamLogo { get; set; } = string.Empty;

        /// <summary>
        /// Дата и время начала матча
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Турнир, по которому идет матч
        /// </summary>
        public Tournament Tournament { get; set; } = new Tournament();
    }
}