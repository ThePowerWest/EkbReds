using System.ComponentModel.DataAnnotations;
using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces;
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
        private readonly IUserService UserService;

        /// <summary>
        /// ctor
        /// </summary>
        public RegisterModel(UserManager<User> userManager, IUserService userService)
        {
            UserManager = userManager;
            UserService = userService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {
        }

        /// <summary>
        /// �����������
        /// </summary>
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new User { Email = Input.Email, UserName = Input.UserName};
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
        /// ������ ����� ������ �� ��������
        /// </summary>
        public class InputModel
        {
            [Required(ErrorMessage ="���� �����������!")]
            [StringLength(20,
                ErrorMessage = "{0} ������ ��������� �� {2} �� {1} ��������.",
                MinimumLength = 4)]
            [Display(Name = "��� ������������")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "���� �����������!")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "���� �����������!")]
            [DataType(DataType.Password)]
            [Display(Name = "������")]
            public string Password { get; set; }
        }
    }
}