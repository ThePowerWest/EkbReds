using ApplicationCore.Entities.Identity;

namespace ApplicationCore.Entities.Main
{
    /// <summary>
    /// Сезоны которые оплатил пользователь и может участвовать в прогнозах
    /// </summary>
    public class SeasonPaid : BaseEntity
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Сезон
        /// </summary>
        public Season Season { get; set; }
    }
}