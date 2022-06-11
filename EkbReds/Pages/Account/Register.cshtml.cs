using System.ComponentModel.DataAnnotations;
using ApplicationCore.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EkbReds.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<User> UserManager;

        /// <summary>
        /// ctor
        /// </summary>
        public RegisterModel(UserManager<User> userManager)
        {
            UserManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {
        }

        /// <summary>
        /// Регистрация
        /// </summary>
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new User { Email = Input.Email, UserName = Input.UserName};
                var result = await UserManager.CreateAsync(user, Input.Password);
                if (result.Succeeded) return LocalRedirect(Url.Content("~/"));
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
            [Required]
            [StringLength(20,
                ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                MinimumLength = 4)]
            [Display(Name = "Имя пользователя")]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(30,
                ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                MinimumLength = 1)]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }
        }
    }
}