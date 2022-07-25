using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EkbReds.Pages
{
    /// <summary>
    /// Главная страница для прогнозов
    /// </summary>
    public class IndexModel : PageModel
    {
        ISeasonRepository SeasonRepository;
        public IEnumerable<Match> Matches; 
        

        /// <summary>
        /// ctor
        /// </summary>
        public IndexModel(ISeasonRepository seasonRepository)
        {
            SeasonRepository = seasonRepository;
        }

        public async Task OnGet() 
        {
            Season currentSeason = await SeasonRepository.LastAsync();
            Matches = currentSeason.Tournaments.SelectMany(tournament => tournament.Matches);
        }
    }
}