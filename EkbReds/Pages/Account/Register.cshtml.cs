using System.ComponentModel.DataAnnotations;
using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EkbReds.Pages.Account
{
    /// <summary>
    /// Страница регистрации
    /// </summary>
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<User> UserManager;
        private readonly IUserService UserService;

        /// <summary>
        /// ctor
        /// </summary>
        public RegisterModel(UserManager<User> userManager, IUserService userService)
        {
            UserManager = userManager;
            UserService = userService;
        }

        /// <summary>
        /// Элемент передачи данных со страницы
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// Регистрация
        /// </summary>
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new User { Email = Input.Email, UserName = Input.UserName };
                var result = await UserManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    await UserService.AddToRoleAsync(user.Id, "User");
                    return LocalRedirect(Url.Content("~/"));
                }
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
        }
    }
}