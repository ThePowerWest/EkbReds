using Newtonsoft.Json;

namespace ApplicationCore.Models
{
    /// <summary>
    /// Название лиги
    /// </summary>
    public class NameTranslation
    {
        /// <summary>
        /// Переведенное название лиги
        /// </summary>
        [JsonProperty("ru")]
        public string Ru { get; set; }
    }
}