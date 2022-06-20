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
    /// �������� � ��������������
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
        /// ������� �������� ������ �� ��������
        /// </summary>
        [BindProperty]
        public InputUserModel Input { get; set; }

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
        /// ������ ����� ������ �� ��������
        /// </summary>
        public class InputUserModel
        {
            /// <summary>
            /// ��� ������������
            /// </summary>
            [Required(ErrorMessage = "���� �����������!")]
            [StringLength(20,
                ErrorMessage = "{0} ������ ��������� �� {2} �� {1} ��������.",
                MinimumLength = 4)]
            [Display(Name = "��� ������������")]
            public string UserName { get; set; }

            /// <summary>
            /// �����
            /// </summary>
            [Required(ErrorMessage = "���� �����������!")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            /// ������
            /// </summary>
            [Required(ErrorMessage = "���� �����������!")]
            [DataType(DataType.Password)]
            [Display(Name = "������")]
            public string Password { get; set; }

            /// <summary>
            /// �����������
            /// </summary>
            [Display(Name = "�����������")]
            public bool EmailConfirmed { get; set; }

            /// <summary>
            /// ����
            /// </summary>
            [Required(ErrorMessage = "���� �����������!")]
            public string Role { get; set; }
        }
    }
}