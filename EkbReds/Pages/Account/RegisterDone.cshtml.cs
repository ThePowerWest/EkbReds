using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Account
{
    /// <summary>
    /// Регистрация успешна
    /// </summary>
    public class RegisterDoneModel : PageModel
    {
        public string FullName;

        /// <summary>
        /// Отобразить страницу с успешной регистрацией
        /// </summary>
        /// <param name="fullName">Полное имя пользователя</param>
        public void OnGet(string fullName)
        {
            FullName = fullName;
        }
    }
}