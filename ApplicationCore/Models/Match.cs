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
        public IEnumerable<Data> Data { get; set; }

        /// <summary>
        /// Ветка Meta
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}