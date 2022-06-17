using Newtonsoft.Json;

namespace ApplicationCore.Models
{
    /// <summary>
    /// Команда
    /// </summary>
    public class Team
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("logo")]
        public string? Logo { get; set; }
    }
}