using ApplicationCore.Entities.Identity;

namespace ApplicationCore.Entities.Main
{
    /// <summary>
    /// Прогнозы
    /// </summary>
    public class Prediction : BaseEntity
    {
        /// <summary>
        /// Пользователь который проставил прогноз
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Матч на который проставлен прогноз
        /// </summary>
        public Match Match { get; set; }

        /// <summary>
        /// Счёт на домашнюю команду
        /// </summary>
        public byte HomeTeamPredict { get; set; }

        /// <summary>
        /// Счёт на выездную команду
        /// </summary>
        public byte AwayTeamPredict { get; set; }
    }
}