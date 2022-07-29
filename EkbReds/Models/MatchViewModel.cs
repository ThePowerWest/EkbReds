using ApplicationCore.Entities.Main;

namespace Web.Models
{
    /// <summary>
    /// Модель отображения данных 
    /// на View для сущности Матча
    /// </summary>
    public class MatchViewModel
    {
        /// <summary>
        /// Идентификатор матча
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название домашней команды
        /// </summary>
        public string HomeTeamName { get; set; }

        /// <summary>
        /// Ссылка на эмблему домашней команды
        /// </summary>
        public string HomeTeamLogo { get; set; }

        /// <summary>
        /// Очки домашней команды
        /// </summary>
        public byte? HomeTeamScore { get; set; }

        /// <summary>
        /// Название выездной команды
        /// </summary>
        public string AwayTeamName { get; set; }

        /// <summary>
        /// Ссылка на эмблему выездной команды
        /// </summary>
        public string? AwayTeamLogo { get; set; }

        /// <summary>
        /// Очки гостевой команды
        /// </summary>
        public byte? AwayTeamScore { get; set; }

        /// <summary>
        /// Дата и время начала матча
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Турнир, по которому идет матч
        /// </summary>
        public Tournament Tournament { get; set; }

        /// <summary>
        /// Прогноз на матч
        /// </summary>
        public PredictionViewModel? Prediction { get; set; }
    }
}