using Newtonsoft.Json;

namespace ApplicationCore.Models
{
    /// <summary>
    /// Матч
    /// </summary>
    public class Match
    {
        /// <summary>
        /// Ветка Data
        /// </summary>
        [JsonProperty("data")]
        public Data[] Data { get; set; }
    }
}