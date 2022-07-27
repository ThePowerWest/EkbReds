using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Account
{
    /// <summary>
    /// ����������� �������
    /// </summary>
    public class RegisterDoneModel : PageModel
    {
        public string FullName;

        /// <summary>
        /// ���������� �������� � �������� ������������
        /// </summary>
        /// <param name="fullName">������ ��� ������������</param>
        public void OnGet(string fullName)
        {
            FullName = fullName;
        }
    }
}