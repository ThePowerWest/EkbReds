using Newtonsoft.Json;

namespace ApplicationCore.Models.SportScore.Teams
{
    internal class Seasons : BaseSportScore
    {
        [JsonProperty("data")]
        internal IEnumerable<Tournament> Data { get; set; }        
    }

    public class Tournament
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("league_id")]
        public int LeagueId { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("year_start")]
        public int? YearStart { get; set; }

        [JsonProperty("year_end")]
        public int? YearEnd { get; set; }
    }
}