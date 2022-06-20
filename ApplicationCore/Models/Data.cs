using Newtonsoft.Json;

namespace ApplicationCore.Models
{
    /// <summary>
    /// Ветка data
    /// </summary>
    public class Data
    {
        /// <summary>
        /// Дата матча
        /// </summary>
        [JsonProperty("start_at")]
        public string Date { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Домашнаяя команда
        /// </summary>
        [JsonProperty("home_team")]
        public Team[] HomeTeam { get; set; }

        /// <summary>
        /// Выездная команда
        /// </summary>
        [JsonProperty("away_team")]
        public Team[] AwayTeam { get; set; }

        /// <summary>
        /// Счет домашней команды
        /// </summary>
        [JsonProperty("home_score")]
        public Score[] HomeScore { get; set; }

        /// <summary>
        /// Счет выездной команды
        /// </summary>
        [JsonProperty("away_score")]
        public Score[] AwayScore { get; set; }

        /// <summary>
        /// Лига
        /// </summary>
        [JsonProperty("league")]
        public League[] League { get; set; }

        /// <summary>
        /// Сезон
        /// </summary>
        [JsonProperty("season")]
        public Season[] Season { get; set; }
    }
}