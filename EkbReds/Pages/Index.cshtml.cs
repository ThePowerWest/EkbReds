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
        public IMUMatches MUMatches;

        /// <summary>
        /// ctor
        /// </summary>
        public IndexModel(IMUMatches muMatches)
        {
            MUMatches = muMatches;
        }

        public async Task OnGet() 
        {
            await MUMatches.GetNextGame();
        }
    }
}