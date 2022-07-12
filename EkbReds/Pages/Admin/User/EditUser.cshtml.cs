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
    /// �������� �������������� ������������
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
        /// ������� �������� ������ �� ��������
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// �������� ������������
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
        /// ������ ����� ������ �� ��������
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// �����
            /// </summary>
            [Required(ErrorMessage = "���� �����������!")]
            [EmailAddress]
            public string Email { get; set; }

            /// <summary>
            /// ��� ������������
            /// </summary>
            [Required(ErrorMessage = "���� �����������!")]
            [Display(Name = "��� ������������")]
            public string UserName { get; set; }

            /// <summary>
            /// ����� ��� ������������
            /// </summary>
            [Required(ErrorMessage = "���� �����������!")]
            [Display(Name = "����� ��� ������������")]
            public string NewUserName { get; set; }

            /// <summary>
            /// �����������
            /// </summary>
            [Display(Name = "�����������")]
            public bool EmailConfirmed { get; set; }

            /// <summary>
            /// ����
            /// </summary>
            [Required(ErrorMessage = "���� �����������!")]
            [Display(Name = "����")]
            public string Role { get; set; }
        }
    }
}