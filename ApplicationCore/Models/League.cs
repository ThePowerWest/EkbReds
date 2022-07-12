using Newtonsoft.Json;

namespace ApplicationCore.Models
{
    /// <summary>
    /// Лига
    /// </summary>
    public class League
    {
        /// <summary>
        /// Название лиги
        /// </summary>
        [JsonProperty("name_translations")]
        public NameTranslation NameTranslation { get; set; }

        /// <summary>
        /// Логотип лиги
        /// </summary>
        [JsonProperty("logo")]
        public string? Logo { get; set; }
    }
}