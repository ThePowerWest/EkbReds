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
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Фамилия
        /// </summary>
        public string SurName { get; set; } = string.Empty;

        /// <summary>
        /// Прогнозы пользователя на матчи
        /// </summary>
        public IEnumerable<Prediction>? Predictions { get; set; }

        /// <summary>
        /// Список сезонов в которых пользователь принимает/принимал/будет принимать участие в прогнозах
        /// </summary>
        public IEnumerable<SeasonPaid>? SeasonPaids { get; set; }
    }
}