using Newtonsoft.Json;

namespace ApplicationCore.Models
{
    /// <summary>
    /// Команда
    /// </summary>
    public class Team
    {
        /// <summary>
        /// Название команды
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Логотип команды
        /// </summary>
        [JsonProperty("logo")]
        public string? Logo { get; set; }
    }
}