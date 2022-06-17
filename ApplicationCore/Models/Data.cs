using Newtonsoft.Json;

namespace ApplicationCore.Models
{
    /// <summary>
    /// Ветка data
    /// </summary>
    public class Data
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("start_at")]
        public string Date { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("home_team")]
        public Team[] HomeTeam { get; set; }

        [JsonProperty("away_team")]
        public Team[] AwayTeam { get; set; }

        [JsonProperty("home_score")]
        public Score[] HomeScore { get; set; }

        [JsonProperty("away_score")]
        public Score[] AwayScore { get; set; }

        [JsonProperty("winner_code")]
        public int[] Winner { get; set; }

        [JsonProperty("league")]
        public League[] League { get; set; }

        [JsonProperty("season")]
        public Season[] Season { get; set; }
    }
}