namespace ApplicationCore.Models.Achievement
{
    /// <summary>
    /// Пользователь который проставил точный прогноз на матч
    /// </summary>
    public class MostAccurateUser
    {
        /// <summary>
        /// ФИО
        /// </summary>
        public string FIO { get; set; }

        /// <summary>
        /// Количество точных прогнозов
        /// </summary>
        public int Point { get; set; }
    }
}