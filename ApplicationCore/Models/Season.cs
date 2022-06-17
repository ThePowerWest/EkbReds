using Newtonsoft.Json;

namespace ApplicationCore.Models
{
    /// <summary>
    /// Сезон
    /// </summary>
    public class Season
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}