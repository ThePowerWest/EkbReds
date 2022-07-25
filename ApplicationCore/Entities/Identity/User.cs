using ApplicationCore.Entities.Main;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Entities.Identity
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : IdentityUser<string>
    {
        public IEnumerable<Prediction>? Predictions { get; set; }
    }
}