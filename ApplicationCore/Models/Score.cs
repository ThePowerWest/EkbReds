using Newtonsoft.Json;
namespace ApplicationCore.Models
{
    /// <summary>
    /// Счет
    /// </summary>
    public class Score
    {
        [JsonProperty("current")]
        public int Current { get; set; }

        [JsonProperty("period_1")]
        public int Period1 { get; set; }

        [JsonProperty("period_2")]
        public int Period2 { get; set; }
    }
}