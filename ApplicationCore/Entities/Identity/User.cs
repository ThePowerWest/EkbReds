using ApplicationCore.Entities.Main;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Entities.Identity
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : IdentityUser<string>
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string SurName { get; set; }

        /// <summary>
        /// Прогнозы пользователя на матчи
        /// </summary>
        public IEnumerable<Prediction>? Predictions { get; set; }
    }
}