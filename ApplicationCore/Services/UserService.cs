using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Services
{
    /// <summary>
    /// Сервис для работы с пользователем
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
            IList<string> roles = await UserManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                await UserManager.RemoveFromRoleAsync(user, role);
            }
            await UserManager.AddToRoleAsync(user, roleName);
        }
    }
}