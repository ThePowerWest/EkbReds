using ApplicationCore.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EkbReds.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User> SignInManager;

            /// <summary>
            /// ��������� ������������
            /// </summary>
        public LogoutModel(SignInManager<User> signInManager)
        {
            SignInManager = signInManager;
        }

            /// <summary>
            /// ����� �� �������
            /// </summary>
        public async Task<IActionResult> OnGet()
        {
            await SignInManager.SignOutAsync();
            return LocalRedirect(Url.Content("~/"));
        }
    }
}