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
        private readonly RoleManager<Role> RoleManager;
        public IEnumerable<Role> Roles;

        /// <summary>
        /// ctor
        /// </summary>
        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager, IEnumerable<Role> roles)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            Roles = roles;
        }

        /// <summary>
        /// Добавляет роль пользователю
        /// </summary>
        public async Task AddToRoleAsync(string userId, string roleName)
        {
            Roles = RoleManager.Roles.ToList();
            var user = await UserManager.FindByIdAsync(userId);
            foreach (var role in Roles)
            {
                try
                {
                    await UserManager.RemoveFromRoleAsync(user, role.Name);
                }
                catch{}
            }
            await UserManager.AddToRoleAsync(user, roleName);
        }
    }
}