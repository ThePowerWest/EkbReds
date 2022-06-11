using ApplicationCore.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages.Admin
{
    public class UsersModel : PageModel
    {
        private readonly UserManager<User> UserManager;
        public IEnumerable<User> Users;

        /// <summary>
        /// ctor
        /// </summary>
        public UsersModel(UserManager<User> userManager, IEnumerable<User> users)
        {
            UserManager = userManager;
            Users = users;
        }

        [BindProperty]
        public InputUserModel Input { get; set; }

        /// <summary>
        /// Получает список пользователей
        /// </summary>
        public void OnGet()
        {
            Users = UserManager.Users.ToList();
        }

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
                var user = new User { Email = Input.Email, UserName = Input.UserName, EmailConfirmed=Input.EmailConfirmed };
                var result = await UserManager.CreateAsync(user, Input.Password);
                if (result.Succeeded) return LocalRedirect(Url.Content("~/Admin/Users"));
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
        public class InputUserModel
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

            [Display(Name = "Подтвержден")]
            public bool EmailConfirmed { get; set; }
        }
    }
}