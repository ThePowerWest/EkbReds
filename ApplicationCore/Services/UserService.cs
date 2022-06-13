using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Services
{
    /// <summary>
    /// Настройка пользователя
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManager<User> UserManager;

        /// <summary>
        /// ctor
        /// </summary>
        public UserService(UserManager<User> userManager)
        {
            UserManager = userManager;
        }

        /// <summary>
        /// Добавляет роль пользователю
        /// </summary>
        public async Task AddToRoleAsync(string userId, string roleName)
        {
            var user = await UserManager.FindByIdAsync(userId);
            await UserManager.AddToRoleAsync(user, roleName);
        }

        public async Task RemoveFromRoleAsync(string userId, string roleName)
        {
            var user = await UserManager.FindByIdAsync(userId);
            await UserManager.RemoveFromRoleAsync(user, roleName);
        }
    }
}