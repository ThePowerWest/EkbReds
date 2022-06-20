using Newtonsoft.Json;
namespace ApplicationCore.Models
{
    /// <summary>
    /// Счет
    /// </summary>
    public class Score
    {
        /// <summary>
        /// Текущий счет
        /// </summary>
        [JsonProperty("current")]
        public int Current { get; set; }
    }
}