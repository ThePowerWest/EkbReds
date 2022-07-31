using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Main;

namespace Web.Models
{
    /// <summary>
    /// Модель отображения данных 
    /// на View для сущности Прогноз
    /// </summary>
    public class PredictionViewModel
    {
        /// <summary>
        /// Идентификатор прогноза
        /// </summary>  
        public int Id { get; set; }

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