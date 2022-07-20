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
        public string HomeTeamName { get; set; }

        /// <summary>
        /// Ссылка на эмблему домашней команды
        /// </summary>
        public string LogoHomeTeam { get; set; }

        /// <summary>
        /// Название выездной команды
        /// </summary>
        public string AwayTeamName { get; set; }

        /// <summary>
        /// Ссылка на эмблему выездной команды
        /// </summary>
        public string LogoAwayTeam { get; set; }

        /// <summary>
        /// Дата и время начала матча
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Список прогнозов участников на матч
        /// </summary>
        public IEnumerable<Prediction> Predictions { get; set; }
    }
}