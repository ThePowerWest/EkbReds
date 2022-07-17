namespace ApplicationCore.Entities.Main
{
    /// <summary>
    /// Сезон
    /// </summary>
    public class Season : BaseEntity
    {
        /// <summary>
        /// Название сезона
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Год старта лиги
        /// </summary>
        public int YearStart { get; set; }

        /// <summary>
        /// Год окончания лиги
        /// </summary>
        public int YearEnd { get; set; }

        /// <summary>
        /// Объект закрыт и больше не может использоваться
        /// </summary>
        public bool IsEnded { get; set; }

        /// <summary>
        /// Текущая лига
        /// </summary>
        public virtual League League { get; set; }

        /// <summary>
        /// Список туров
        /// </summary>
        //public virtual IEnumerable<Tour> Tours { get; set; }
    }
}