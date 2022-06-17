using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class MatchesModel : PageModel
    {
        private readonly IMatchLoadService MatchLoadService;
        public IList<Match> Matches;

        /// <summary>
        /// ctor
        /// </summary>
        public MatchesModel(IMatchLoadService matchLoadService, IList<Match> matches)
        {
            Matches = matches;
            MatchLoadService = matchLoadService;
        }

        /// <summary>
        /// Получение списка матчей
        /// </summary>
        public async void OnGet()
        {
            Matches = await MatchLoadService.LoadAsync();
        }
    }
}
