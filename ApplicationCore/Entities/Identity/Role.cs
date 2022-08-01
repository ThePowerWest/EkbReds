using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Entities.Identity
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public class Role : IdentityRole<string>
    {
        /// <summary>
        /// Описание роли
        /// </summary>
        public string Description { get; set; }
    }
}