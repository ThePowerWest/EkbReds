using Newtonsoft.Json;

namespace ApplicationCore.Models.SportScore.Teams
{
    /// <summary>
    /// Сезон
    /// </summary>
    public class Seasons : BaseSportScore
    {
        /// <summary>
        /// Список сезонов
        /// </summary>
        [JsonProperty("data")]
        public IEnumerable<Tournament> Data { get; set; }        
    }

    /// <summary>
    /// Турнир
    /// </summary>
    public class Tournament
    {
        /// <summary>
        /// Идентификатор турнира
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор лиги
        /// </summary>
        [JsonProperty("league_id")]
        public int LeagueId { get; set; }

        /// <summary>
        /// Года в которые проводится данный турнир
        /// </summary>
        [JsonProperty("slug")]
        public string Slug { get; set; }

        /// <summary>
        /// Наименование турнира
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Год начала турнира
        /// </summary>
        [JsonProperty("year_start")]
        public int? YearStart { get; set; }

        /// <summary>
        /// Год окончания турнира
        /// </summary>
        [JsonProperty("year_end")]
        public int? YearEnd { get; set; }
    }
}