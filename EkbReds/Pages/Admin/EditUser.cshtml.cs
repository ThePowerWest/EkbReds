using ApplicationCore.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages.Admin
{
    public class EditUserModel : PageModel
    {
        private readonly UserManager<User> UserManager;

        /// <summary>
        /// ctor
        /// </summary>
        public EditUserModel(UserManager<User> userManager)
        {
            UserManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {
        }

        /// <summary>
        /// Изменяет пользователя
        /// </summary>
        public async Task<IActionResult> OnPostEditUserAsync()
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.Users.FirstOrDefault(x => x.UserName == Input.UserName);
                if (Input.NewUserName != null) user.UserName = Input.NewUserName;
                user.Email=Input.Email;
                user.EmailConfirmed = Input.EmailConfirmed;
                var result = await UserManager.UpdateAsync(user);
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
        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Имя пользователя")]
            public string UserName { get; set; }
            
            [Display(Name = "Новое Имя пользователя")]
            public string NewUserName { get; set; }

            [Display(Name = "Подтвержден")]
            public bool EmailConfirmed { get; set; }
        }
    }
}