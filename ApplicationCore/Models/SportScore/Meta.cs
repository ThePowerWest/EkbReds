using Newtonsoft.Json;

namespace ApplicationCore.Models.SportScore
{
    /// <summary>
    /// Дополнительные данные SportScore
    /// </summary>
    public class Meta
    {
        /// <summary>
        /// Текущая страница
        /// </summary>
        [JsonProperty("current_page")]
        public byte CurrentPage { get; set; }

        /// <summary>
        /// Сколько всего страниц
        /// </summary>
        [JsonProperty("from")]
        public byte From { get; set; }

        /// <summary>
        /// Сколько всего объектов на странице
        /// </summary>
        [JsonProperty("per_page")]
        public byte PerPage { get; set; }

        /// <summary>
        /// Сколько максимум объектов на странице
        /// </summary>
        [JsonProperty("to")]
        public byte To { get; set; }
    }
}