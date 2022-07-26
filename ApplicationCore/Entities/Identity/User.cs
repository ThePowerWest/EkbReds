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
        /// Прогнозы пользователя на матчи
        /// </summary>
        public IEnumerable<Prediction>? Predictions { get; set; }
    }
}