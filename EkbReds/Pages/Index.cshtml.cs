using ApplicationCore.Interfaces.SportScore;
using ApplicationCore.Models.SportScore.Teams;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EkbReds.Pages
{
    /// <summary>
    /// Главная страница
    /// </summary>
    public class IndexModel : PageModel
    {
        public IMUMatches MUMatches;
        public List<EventData> Matches;

        /// <summary>
        /// ctor
        /// </summary>
        public IndexModel(IMUMatches muMatches)
        {
            MUMatches = muMatches;
        }

        public async Task OnGet() 
        {
            Matches = await MUMatches.GetNextGames();
        }
    }
}