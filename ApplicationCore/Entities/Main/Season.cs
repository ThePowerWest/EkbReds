namespace ApplicationCore.Entities.Main
{
    /// <summary>
    /// Сезон
    /// </summary>
    public class Season : BaseEntity
    {
        /// <summary>
        /// Год начала сезона
        /// </summary>
        public int YearStart { get; set; }

        /// <summary>
        /// Год окончания сезона
        /// </summary>
        public int YearEnd { get; set; }

        /// <summary>
        /// Список турниров в сезоне
        /// </summary>
        public IEnumerable<Tournament> Tournaments { get; set; }
    }
}