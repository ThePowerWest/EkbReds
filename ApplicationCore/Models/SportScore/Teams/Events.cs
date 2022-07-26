using ApplicationCore.Models.SportScore.Objects;
using Newtonsoft.Json;

namespace ApplicationCore.Models.SportScore.Teams
{
    /// <summary>
    /// Событие
    /// </summary>
    public class Events : BaseSportScore
    {
        /// <summary>
        /// Список событий
        /// </summary>
        [JsonProperty("data")]
        public IEnumerable<EventData> Data { get; set; }
    }

    /// <summary>
    /// Матч
    /// </summary>
    public class EventData
    {
        /// <summary>
        /// Идентификатор сезона
        /// </summary>
        [JsonProperty("season_id")]
        public int? SeasonId { get; set; }

        /// <summary>
        /// Текущий статус матча
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Дата и время начала матча
        /// </summary>
        [JsonProperty("start_at")]
        public DateTime StartAt { get; set; }

        /// <summary>
        /// Домашняя команда
        /// </summary>
        [JsonProperty("home_team")]
        public Team HomeTeam { get; set; }

        /// <summary>
        /// Гостевая команда
        /// </summary>
        [JsonProperty("away_team")]
        public Team AwayTeam { get; set; }

        /// <summary>
        /// Турнир
        /// </summary>
        [JsonProperty("season")]
        public Tournament Tournament { get; set; }
    }
}