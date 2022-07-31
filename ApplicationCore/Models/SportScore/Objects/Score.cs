using Newtonsoft.Json;

namespace ApplicationCore.Models.SportScore.Objects
{
    /// <summary>
    /// Очки команд
    /// </summary>
    public class Score
    {
        /// <summary>
        /// Количество голов
        /// </summary>
        [JsonProperty("current")]
        public byte Current { get; set; }
    }
}