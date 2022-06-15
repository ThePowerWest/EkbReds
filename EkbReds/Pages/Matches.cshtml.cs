using ApplicationCore.Entities.DTO;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class MatchesModel : PageModel
    {
        private readonly IMatchLoadService MatchLoadService;
        public IList<Match> Matches;

        public MatchesModel(IMatchLoadService matchLoadService, IList<Match> matches)
        {
            Matches = matches;
            MatchLoadService = matchLoadService;
        }

        public async void OnGet()
        {
            Matches = await MatchLoadService.LoadAsync();
        }
    }
}
