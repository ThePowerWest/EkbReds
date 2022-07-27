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
    /// �������� �����������
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
        /// ������ ��� ����������� ������������
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// ����� ������������
            /// </summary>
            [Required(ErrorMessage = "���� �����������!")]
            [Display(Name = "�����")]
            public string UserName { get; set; }

            /// <summary>
            /// ���
            /// </summary>
            [Required(ErrorMessage = "���� �����������!")]
            [Display(Name = "���")]
            public string FirstName { get; set; }

            /// <summary>
            /// �������
            /// </summary>
            [Required(ErrorMessage = "���� �����������!")]
            [Display(Name = "�������")]
            public string SurName { get; set; }

            /// <summary>
            /// ������
            /// </summary>
            [Required(ErrorMessage = "���� �����������!")]
            [DataType(DataType.Password)]
            [Display(Name = "������")]
            public string Password { get; set; }
        }

        /// <summary>
        /// �����������
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