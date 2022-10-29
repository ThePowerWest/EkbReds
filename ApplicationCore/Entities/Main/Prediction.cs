using ApplicationCore.Entities.Base;
using ApplicationCore.Entities.Identity;

namespace ApplicationCore.Entities.Main
{
    /// <summary>
    /// Прогнозы
    /// </summary>
    public class Prediction : BaseEntity
    {
        /// <summary>
        /// Дата проставления прогноза
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Пользователь который проставил прогноз
        /// </summary>
        public User User { get; set; } = new User();

        /// <summary>
        /// Матч на который проставлен прогноз
        /// </summary>
        public Match Match { get; set; } = new Match();

        /// <summary>
        /// Счёт на домашнюю команду
        /// </summary>
        public byte HomeTeamPredict { get; set; }

        /// <summary>
        /// Счёт на выездную команду
        /// </summary>
        public byte AwayTeamPredict { get; set; }

        /// <summary>
        /// Количество очков которое пользователь получил за текущий прогноз
        /// </summary>
        public byte? Point { get; set; }
    }
}