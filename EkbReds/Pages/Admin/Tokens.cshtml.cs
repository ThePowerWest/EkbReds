using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class TokensModel : PageModel
    {
        private readonly IRepository<ApiToken> TokenRepository;
        public IEnumerable<ApiToken> Tokens;

        /// <summary>
        /// ctor
        /// </summary>
        public TokensModel(IEnumerable<ApiToken> tokens, IRepository<ApiToken> tokenRepository)
        {
            Tokens = tokens;
            TokenRepository = tokenRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// Получение списка токенов
        /// </summary>
        public async Task OnGet()
        {
            Tokens = await TokenRepository.ListAsync();
        }

        /// <summary>
        /// Добавление токена
        /// </summary>
        public async Task<IActionResult> OnPostAddTokenAsync()
        {
            if (ModelState.IsValid)
            {
                await TokenRepository.AddAsync(new ApiToken { Key= Input.Key});
                return LocalRedirect(Url.Content("~/Admin/Tokens"));
            }
            return Page();
        }

        /// <summary>
        /// Удаление токена
        /// </summary>
        public async Task<IActionResult> OnPostRemoveTokenAsync(int id)
        {
            var token = await TokenRepository.GetByIdAsync(id);
            if (token != null)
            {
                await TokenRepository.DeleteAsync(token);
                return LocalRedirect(Url.Content("~/Admin/Tokens"));
            }
            return Page();
        }

        /// <summary>
        /// Модель ввода данных со страницы
        /// </summary>
        public class InputModel 
        {
            [Required(ErrorMessage = "Поле обязательно!")]
            [Display(Name = "Ключ")]
            public string Key { get; set; }
        }
    }
}