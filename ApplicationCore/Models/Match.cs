using Newtonsoft.Json;

namespace ApplicationCore.Models
{
    /// <summary>
    /// Матч
    /// </summary>
    public class Match
    {
        [JsonProperty("data")]
        public Data[] Data { get; set; }
    }  
}
