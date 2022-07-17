using Newtonsoft.Json;

namespace ApplicationCore.Models.SportScore.Teams
{
    internal class Seasons : BaseSportScore
    {
        [JsonProperty("data")]
        internal IEnumerable<SeasonData> Data { get; set; }        
    }

    internal class SeasonData
    {
        [JsonProperty("id")]
        internal int Id { get; set; }

        [JsonProperty("league_id")]
        internal int LeagueId { get; set; }

        [JsonProperty("slug")]
        internal string Slug { get; set; }

        [JsonProperty("name")]
        internal string Name { get; set; }

        [JsonProperty("year_start")]
        internal string YearStart { get; set; }

        [JsonProperty("year_end")]
        internal string YearEnd { get; set; }
    }
}