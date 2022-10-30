using ApplicationCore.Entities.Base;

namespace ApplicationCore.Entities.Main
{
    /// <summary>
    /// Турнир
    /// </summary>
    public class Tournament : BaseEntity
    {
        /// <summary>
        /// Название турнира
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Текущий сезон
        /// </summary>
        public Season Season { get; set; } = new Season();

        /// <summary>
        /// Список матчей в турнире
        /// </summary>
        public IEnumerable<Match>? Matches { get; set; }
    }
}