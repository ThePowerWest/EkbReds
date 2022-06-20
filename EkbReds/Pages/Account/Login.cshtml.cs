using System.ComponentModel.DataAnnotations;
using ApplicationCore.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EkbReds.Pages.Account
{
    /// <summary>
    /// �������� �����������
    /// </summary>
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> SignInManager;

        /// <summary>
        /// ������� �������� ������ �� ��������
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public LoginModel(SignInManager<User> signInManager)
        {
            SignInManager = signInManager;
        }

        /// <summary>
        /// ���� � �������
        /// </summary>
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, false);
                if (result.Succeeded) return LocalRedirect(Url.Content("~/"));
                if (result.IsLockedOut) return Page();
                else
                {
                    ModelState.AddModelError(string.Empty, "������ �����������");
                    return Page();
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
            /// ��� ������������
            /// </summary>
            [Required(ErrorMessage = "���� �����������!")]
            [Display(Name = "��� ������������")]
            public string UserName { get; set; }

            /// <summary>
            /// ������
            /// </summary>
            [Required(ErrorMessage = "���� �����������!")]
            [DataType(DataType.Password)]
            [Display(Name = "������")]
            public string Password { get; set; }

            /// <summary>
            /// ��������� ������������
            /// </summary>
            [Display(Name = "��������� ����")]
            public bool RememberMe { get; set; }
        }
    }
}