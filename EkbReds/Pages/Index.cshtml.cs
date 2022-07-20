using ApplicationCore.Models.SportScore.Teams;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EkbReds.Pages
{
    /// <summary>
    /// Главная страница
    /// </summary>
    public class IndexModel : PageModel
    {
        public List<EventData> Matches;

        /// <summary>
        /// ctor
        /// </summary>
        public IndexModel()
        {
        }

        public async Task OnGet() 
        {
        }
    }
}