using Newtonsoft.Json;

namespace ApplicationCore.Models
{
    public class Meta
    {
        /// <summary>
        /// Текущая страница
        /// </summary>
        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }
    }
}