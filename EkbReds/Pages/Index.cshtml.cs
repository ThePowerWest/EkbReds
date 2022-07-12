using ApplicationCore.Interfaces.SportScore;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EkbReds.Pages
{
    /// <summary>
    /// Главная страница
    /// </summary>
    public class IndexModel : PageModel
    {
        public IMatchLoadService MatchLoadService;
        public Match Matches;

        /// <summary>
        /// ctor
        /// </summary>
        public IndexModel(IMatchLoadService matchLoadService)
        {
            MatchLoadService = matchLoadService;
        }

        public async Task OnGet() 
        {
        }
    }
}