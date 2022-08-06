using ApplicationCore.Entities.Identity;

namespace ApplicationCore.Models
{
    /// <summary>
    /// Модель для отображения достижений
    /// </summary>
    public class UserCountModel
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Количество чего либо
        /// </summary>
        public int Count { get; set; }
    }
}