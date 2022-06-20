using Newtonsoft.Json;

namespace ApplicationCore.Models
{
    /// <summary>
    /// Сезон
    /// </summary>
    public class Season
    {
        /// <summary>
        /// Название сезона
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}