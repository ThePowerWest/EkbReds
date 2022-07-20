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
        public string Name { get; set; }

        /// <summary>
        /// Текущий сезон
        /// </summary>
        public Season Season { get; set; }

        /// <summary>
        /// Список матчей в турнире
        /// </summary>
        public IEnumerable<Match> Matches { get; set; }
    }
}
