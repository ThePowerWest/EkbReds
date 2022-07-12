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
    /// Страница редактирования пользователя
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class EditUserModel : PageModel
    {
        private readonly UserManager<User> UserManager;
        public RoleManager<Role> RoleManager;
        private readonly IUserService UserService;

        /// <summary>
        /// ctor
        /// </summary>
        public EditUserModel(UserManager<User> userManager, RoleManager<Role> roleManager, IUserService userService)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            UserService = userService;
        }

        /// <summary>
        /// Элемент передачи данных со страницы
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// Изменяет пользователя
        /// </summary>
        public async Task<IActionResult> OnPostEditUserAsync()
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.Users.FirstOrDefault(x => x.UserName == Input.UserName);
                if (Input.NewUserName != null) user.UserName = Input.NewUserName;
                user.Email = Input.Email;
                user.EmailConfirmed = Input.EmailConfirmed;
                await UserService.AddToRoleAsync(user.Id, Input.Role);
                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded) return LocalRedirect(Url.Content("~/Admin/User/Users"));
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }

        /// <summary>
        /// Модель ввода данных со страницы
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Почта
            /// </summary>
            [Required(ErrorMessage = "Поле обязательно!")]
            [EmailAddress]
            public string Email { get; set; }

            /// <summary>
            /// Имя пользователя
            /// </summary>
            [Required(ErrorMessage = "Поле обязательно!")]
            [Display(Name = "Имя пользователя")]
            public string UserName { get; set; }

            /// <summary>
            /// Новое имя пользователя
            /// </summary>
            [Required(ErrorMessage = "Поле обязательно!")]
            [Display(Name = "Новое Имя пользователя")]
            public string NewUserName { get; set; }

            /// <summary>
            /// Подтвержден
            /// </summary>
            [Display(Name = "Подтвержден")]
            public bool EmailConfirmed { get; set; }

            /// <summary>
            /// Роль
            /// </summary>
            [Required(ErrorMessage = "Поле обязательно!")]
            [Display(Name = "Роль")]
            public string Role { get; set; }
        }
    }
}