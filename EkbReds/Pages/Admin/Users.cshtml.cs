using ApplicationCore.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages.Admin
{
    public class UsersModel : PageModel
    {
        public UserManager<User> UserManager;
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
        /// �������� ������ �������������
        /// </summary>
        public void OnGet()
        {
            Users = UserManager.Users.ToList();
        }

        /// <summary>
        /// ������� ������������
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
        /// ��������� ������������
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
        /// ������ ����� ������ �� ��������
        /// </summary>
        public class InputUserModel
        {
            [Required(ErrorMessage = "���� �����������!")]
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

            [Display(Name = "�����������")]
            public bool EmailConfirmed { get; set; }

            public string Role { get; set; }
        }
    }
}