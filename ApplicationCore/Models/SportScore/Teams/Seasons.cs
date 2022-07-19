using Newtonsoft.Json;

namespace ApplicationCore.Models.SportScore.Teams
{
    internal class Seasons : BaseSportScore
    {
        [JsonProperty("data")]
        internal IEnumerable<SeasonData> Data { get; set; }        
    }

    public class SeasonData
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
        public string YearStart { get; set; }

        [JsonProperty("year_end")]
        public string YearEnd { get; set; }
    }
}