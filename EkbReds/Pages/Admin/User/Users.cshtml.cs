using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages.Admin
{
    /// <summary>
    /// Страница с пользователями
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class UsersModel : PageModel
    {
        public UserManager<User> UserManager;
        public RoleManager<Role> RoleManager;
        private readonly IUserService UserService;

        /// <summary>
        /// ctor
        /// </summary>
        public UsersModel(UserManager<User> userManager, RoleManager<Role> roleManager, IUserService userService)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            UserService = userService;
        }

        /// <summary>
        /// Элемент передачи данных со страницы
        /// </summary>
        [BindProperty]
        public InputUserModel Input { get; set; }

        /// <summary>
        /// Удаляет пользователя
        /// </summary>
        public async Task<IActionResult> OnPostDeleteUserAsync(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                User user = await UserManager.FindByIdAsync(id);
                if (user != null)
                {
                    IdentityResult result = await UserManager.DeleteAsync(user);
                    if (result.Succeeded) return LocalRedirect(Url.Content("~/Admin/Users"));
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return Page();
        }

        /// <summary>
        /// Добавляет пользователя
        /// </summary>
        public async Task<IActionResult> OnPostAddUserAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new User { Email = Input.Email, UserName = Input.UserName, EmailConfirmed = Input.EmailConfirmed };
                var result = await UserManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    await UserService.AddToRoleAsync(user.Id, Input.Role);
                    return LocalRedirect(Url.Content("~/Admin/Users"));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return Page();
        }

        /// <summary>
        /// Модель ввода данных со страницы
        /// </summary>
        public class InputUserModel
        {
            /// <summary>
            /// Имя пользователя
            /// </summary>
            [Required(ErrorMessage = "Поле обязательно!")]
            [StringLength(20,
                ErrorMessage = "{0} должно содержать от {2} до {1} символов.",
                MinimumLength = 4)]
            [Display(Name = "Имя пользователя")]
            public string UserName { get; set; }

            /// <summary>
            /// Почта
            /// </summary>
            [Required(ErrorMessage = "Поле обязательно!")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            /// Пароль
            /// </summary>
            [Required(ErrorMessage = "Поле обязательно!")]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            /// <summary>
            /// Подтвержден
            /// </summary>
            [Display(Name = "Подтвержден")]
            public bool EmailConfirmed { get; set; }

            /// <summary>
            /// Роль
            /// </summary>
            [Required(ErrorMessage = "Поле обязательно!")]
            public string Role { get; set; }
        }
    }
}