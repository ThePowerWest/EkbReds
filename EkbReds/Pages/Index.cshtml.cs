using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Managers;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Models;

namespace EkbReds.Pages
{
    /// <summary>
    /// Главная страница для прогнозов
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly UserManagerEx UserManager;
        private readonly IMatchRepository MatchRepository;
        private readonly IUserRepository UserRepository;
        private readonly IPredictionRepository _predictionRepository;

        public NextMatch? NextMatch;
        public IEnumerable<PreviousMatch> PreviousMatches;
        public IEnumerable<FutureMatch> FutureMatches;
        public IEnumerable<TopUser> PointTable;
        public IEnumerable<User> EkbRedsUsers;

        /// <summary>
        /// ctor
        /// </summary>
        public IndexModel(
            UserManagerEx userManager,
            IMatchRepository matchRepository,
            IUserRepository userRepository,
            IPredictionRepository predictionRepository)
        {
            UserManager = userManager;
            MatchRepository = matchRepository;
            UserRepository = userRepository;
            _predictionRepository = predictionRepository;
        }

        /// <summary>
        /// Отобразить страницу с прогнозами
        /// </summary>
        public async Task OnGet()
        {          
            User currentUser = await UserManager.GetUserAsync(User);
            if (currentUser == null)
            {
                NextMatch = await MatchRepository.NextAsync();
                PreviousMatches = await MatchRepository.PreviousAsync(3);
            }
            else
            {
                NextMatch = await MatchRepository.NextAsync(currentUser);
                PreviousMatches = await MatchRepository.PreviousAsync(3, currentUser.Id);
            }

            if(NextMatch != null)
            {
                NextMatch.StartDate = NextMatch.StartDate.AddHours(5);
                NextMatch.PredictionEndDate = NextMatch.PredictionEndDate.AddHours(3);
            }

            EkbRedsUsers = await UserRepository.RandomUsersAsync(4);
            FutureMatches = await MatchRepository.FutureAsync(3);

            PointTable = await _predictionRepository.TopUsersAsync(10);
        }

        /// <summary>
        /// Создать прогноз на выбранный матч 
        /// </summary>
        /// <param name="match"></param>
        /// <returns>Вернуться на главную страницу</returns>
        public async Task<IActionResult> OnPostMakePredictionAsync(MatchViewModel match)
        {
            await _predictionRepository.CreateOrUpdateAsync(
                User.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value,
                match.Prediction.HomeTeamPredict,
                match.Prediction.AwayTeamPredict);

            return LocalRedirect(Url.Content("~/"));
        }
    }
}