using Newtonsoft.Json;

namespace ApplicationCore.Models
{
    /// <summary>
    /// Название лиги
    /// </summary>
    public class NameTranslation
    {
        [JsonProperty("ru")]
        public string Ru { get; set; }
    }
}