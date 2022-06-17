using Newtonsoft.Json;

namespace ApplicationCore.Models
{
    /// <summary>
    /// Лига
    /// </summary>
    public class League
    {
        [JsonProperty("name_translations")]
        public NameTranslation[] NameTranslation { get; set; }

        [JsonProperty("logo")]
        public string? Logo { get; set; }
    }
}