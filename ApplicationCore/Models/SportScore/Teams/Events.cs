using ApplicationCore.Models.SportScore.Objects;
using Newtonsoft.Json;

namespace ApplicationCore.Models.SportScore.Teams
{
    internal class Events : BaseSportScore
    {
        [JsonProperty("data")]
        internal IEnumerable<EventData> Data { get; set; }
    }

    public class EventData
    {
        [JsonProperty("season_id")]
        public int? SeasonId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("start_at")]
        public DateTime StartAt { get; set; }

        [JsonProperty("home_team")]
        public Team HomeTeam { get; set; }

        [JsonProperty("away_team")]
        public Team AwayTeam { get; set; }
    }
}