namespace ApplicationCore.Entities.Main
{
    /// <summary>
    /// Лига
    /// </summary>
    public class League : BaseEntity
    {
        /// <summary>
        /// Название лиги
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список сезонов
        /// </summary>
        public virtual IEnumerable<Season> Seasons { get; set; }

        /// <summary>
        /// Объект закрыт и больше не может использоваться
        /// </summary>
        public bool IsEnded { get; set; }
    }
}