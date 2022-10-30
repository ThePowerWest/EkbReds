using ApplicationCore.Entities.Base;

namespace ApplicationCore.Entities.Main
{
    /// <summary>
    /// Матч
    /// </summary>
    public class Match : BaseEntity
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
        /// Количество голов, которые забила домашняя команда
        /// </summary>
        public byte? HomeTeamScore { get; set; }

        /// <summary>
        /// Название выездной команды
        /// </summary>
        public string AwayTeamName { get; set; } = string.Empty;

        /// <summary>
        /// Ссылка на эмблему выездной команды
        /// </summary>
        public string AwayTeamLogo { get; set; } = string.Empty;

        /// <summary>
        /// Количество голов, которые забила выездная команда
        /// </summary>
        public byte? AwayTeamScore { get; set; }

        /// <summary>
        /// Дата и время начала матча
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Турнир, по которому идет матч
        /// </summary>
        public Tournament Tournament { get; set; } = new Tournament();

        /// <summary>
        /// Список прогнозов
        /// </summary>
        public IEnumerable<Prediction>? Predictions { get; set; }

        /// <summary>
        /// Статус матча
        /// </summary>
        public MatchStatusId MatchStatusId { get; set; } = MatchStatusId.NotStarted;
    }
}