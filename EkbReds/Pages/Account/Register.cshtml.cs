using System.ComponentModel.DataAnnotations;
using System.Net;
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
        public RegisterModel(
            UserManager<User> userManager,
            IUserService userService)
        {
            UserManager = userManager;
            UserService = userService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// Данные для регистрации пользователя
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Логин пользователя
            /// </summary>
            [Required(ErrorMessage = "Поле обязательно!")]
            [Display(Name = "Логин")]
            public string UserName { get; set; }

            /// <summary>
            /// Имя
            /// </summary>
            [Required(ErrorMessage = "Поле обязательно!")]
            [Display(Name = "Имя")]
            public string FirstName { get; set; }

            /// <summary>
            /// Фамилия
            /// </summary>
            [Required(ErrorMessage = "Поле обязательно!")]
            [Display(Name = "Фамилия")]
            public string SurName { get; set; }

            /// <summary>
            /// Пароль
            /// </summary>
            [Required(ErrorMessage = "Поле обязательно!")]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }
        }

        /// <summary>
        /// Регистрация
        /// </summary>
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = Input.UserName + "@ekaterinburgreds.ru",
                    UserName = Input.UserName,
                    FirstName = Input.FirstName,
                    SurName = Input.SurName
                };

                IdentityResult result = await UserManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, "User");
                    return LocalRedirect(Url.Content($"~/Account/RegisterDone?" +
                        $"fullName={ WebUtility.UrlEncode(user.FirstName + " " + user.SurName) }"));
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}