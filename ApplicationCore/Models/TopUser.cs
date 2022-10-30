namespace ApplicationCore.Models
{
    /// <summary>
    /// Пользователи набравшие больше всего очков в прогнозаъ
    /// </summary>
    public class TopUser
    {
        /// <summary>
        /// ФИО пользователя
        /// </summary>
        public string FIO { get; set; }

        /// <summary>
        /// Сумма очков которые набрал пользователь за все прогнозы на этот сезон
        /// </summary>
        public int Point { get; set; }
    }
}