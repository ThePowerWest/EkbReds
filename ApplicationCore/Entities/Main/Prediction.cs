using ApplicationCore.Entities.Identity;

namespace ApplicationCore.Entities.Main
{
    public class Prediction
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Матч
        /// </summary>
        public virtual Match Match { get; set; }

        /// <summary>
        /// Прогноз на домашнюю команду
        /// </summary>
        public byte HomeTeamPredict { get; set; }

        /// <summary>
        /// Прогноз на выездную команду
        /// </summary>
        public byte AwayTeamPredict { get; set; }
    }
}