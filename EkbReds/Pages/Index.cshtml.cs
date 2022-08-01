using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.Models;
using static Web.Services.ScoringService;

namespace EkbReds.Pages
{
    /// <summary>
    /// Главная страница для прогнозов
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> UserManager;
        private readonly IRepository<Prediction> PredictionCRUDRepository;
        private readonly IMatchRepository MatchRepository;
        private readonly IRepository<Match> MatchCRUDRepository;
        private readonly IPredictionRepository PredictionRepository;
        private readonly IScoringService ScoringService;

        public MatchViewModel NextMatch;
        public IEnumerable<MatchViewModel> ThreeAfterNextMatches;
        public IEnumerable<MatchViewModel> ThreeBeforeNextMatches;
        public IEnumerable<MatchViewModel> Matches;
        public IEnumerable<PointTable> PointTable;

        /// <summary>
        /// ctor
        /// </summary>
        public IndexModel(
            IRepository<Prediction> predictionCRUDRepository,
            UserManager<User> userManager,
            IMatchRepository matchRepository,
            IPredictionRepository predictionRepository,
            IRepository<Match> matchCRUDRepository,
            IScoringService scoringService)
        {
            PredictionCRUDRepository = predictionCRUDRepository;
            UserManager = userManager;
            MatchRepository = matchRepository;
            PredictionRepository = predictionRepository;
            MatchCRUDRepository = matchCRUDRepository;
            ScoringService = scoringService;
        }

        /// <summary>
        /// Отобразить страницу с прогнозами
        /// </summary>
        public async Task OnGet()
        {
            User currentUser = await UserManager.GetUserAsync(User);
            IEnumerable<Match> matches = await MatchRepository.IndexList(currentUser);
            matches = ConvertTimeToMatches(matches);
            NextMatch = GetNextMatch(matches);
            NextMatch.EndTimeForPrediction = NextMatch.StartDate.AddHours(-2);
            ThreeAfterNextMatches = GetThreeMatchesAfterNext(matches);
            ThreeBeforeNextMatches = GetThreeMatchesBeforeNext(matches);

            PointTable = ScoringService.TopPredictionsByUsers(await UserManager.GetUsersInRoleAsync(RolesModel.User.ToString()), NextMatch.Tournament.Season)
                                       .OrderByDescending(point => point.Points)
                                       .Take(10);
        }

        /// <summary>
        /// Создать прогноз на выбранный матч 
        /// </summary>
        public async Task<IActionResult> OnPostMakePredictionAsync(MatchViewModel match)
        {
            Match currentMatch = await MatchCRUDRepository.GetByIdAsync(match.Id);

            if (DateTime.Now >= currentMatch.StartDate.AddHours(3)) return LocalRedirect(Url.Content("~/")); ;

            // TODO переделать на один запрос в БД
            if (match.Prediction.Id != 0)
            {
                Prediction prediction = await PredictionCRUDRepository.GetByIdAsync(match.Prediction.Id);
                prediction.AwayTeamPredict = match.Prediction.AwayTeamPredict;
                prediction.HomeTeamPredict = match.Prediction.HomeTeamPredict;

                await PredictionCRUDRepository.UpdateAsync(prediction);
            }
            else
            {
                
                await PredictionCRUDRepository.AddAsync(
                    new Prediction
                    {
                        Match = currentMatch,
                        User = await UserManager.GetUserAsync(User),
                        HomeTeamPredict = match.Prediction.HomeTeamPredict,
                        AwayTeamPredict = match.Prediction.AwayTeamPredict
                    });
            }

            return LocalRedirect(Url.Content("~/"));
        }

        private IEnumerable<Match> ConvertTimeToMatches(IEnumerable<Match> matches)
        {
            foreach(Match match in matches)
            {
                match.StartDate = match.StartDate.AddHours(5);
            }

            return matches;
        }

        /// <summary>
        /// Получить список прошедших матчей до следующего
        /// </summary>
        /// <param name="matches">Список матчей</param>
        /// <returns>Список матчей</returns>
        private IEnumerable<MatchViewModel> GetThreeMatchesBeforeNext(IEnumerable<Match> matches)
                        => matches
                                .Where(match => DateTime.Now > match.StartDate)
                                .Take(3)
                                .Select(match => new MatchViewModel
                                  {
                                      Id = match.Id,
                                      HomeTeamName = match.HomeTeamName,
                                      HomeTeamLogo = match.HomeTeamLogo,
                                      HomeTeamScore = match.HomeTeamScore,
                                      AwayTeamName = match.AwayTeamName,
                                      AwayTeamLogo = match.AwayTeamLogo,
                                      AwayTeamScore = match.AwayTeamScore,
                                      StartDate = match.StartDate,
                                      Tournament = match.Tournament,
                                      Prediction = match.Predictions != null ? match.Predictions
                                                           .Select(prediction => new PredictionViewModel
                                                           {
                                                               AwayTeamPredict = prediction.AwayTeamPredict,
                                                               HomeTeamPredict = prediction.HomeTeamPredict
                                                           }).FirstOrDefault() : null
                        });

        /// <summary>
        /// Получить три следующий матчей после следующего
        /// </summary>
        /// <param name="matches">Список матчей</param>
        /// <returns>Список матчей</returns>
        private IEnumerable<MatchViewModel> GetThreeMatchesAfterNext(IEnumerable<Match> matches)
                        => matches.Where(match => DateTime.Now < match.StartDate)
                                  .Skip(1)
                                  .Select(match => new MatchViewModel
                                  {
                                      Id = match.Id,
                                      HomeTeamName = match.HomeTeamName,
                                      HomeTeamLogo = match.HomeTeamLogo,
                                      HomeTeamScore = match.HomeTeamScore,
                                      AwayTeamName = match.AwayTeamName,
                                      AwayTeamLogo = match.AwayTeamLogo,
                                      AwayTeamScore = match.AwayTeamScore,
                                      StartDate = match.StartDate,
                                      Tournament = match.Tournament,
                                      Prediction = null
                                  });

        /// <summary>
        /// Получить следующий матч по порядку и добавить прогноз только от текущего пользователя
        /// </summary>
        /// <param name="matches">Список матчей</param>
        /// <param name="currentUser">Текущий пользователь</param>
        /// <returns>Следующий матч</returns>
        private MatchViewModel GetNextMatch(IEnumerable<Match> matches)
                    => matches.Where(match => DateTime.Now < match.StartDate)
                                       .Select(match => new MatchViewModel
                                       {
                                           Id = match.Id,
                                           HomeTeamName = match.HomeTeamName,
                                           HomeTeamLogo = match.HomeTeamLogo,
                                           HomeTeamScore = match.HomeTeamScore,
                                           AwayTeamName = match.AwayTeamName,
                                           AwayTeamLogo = match.AwayTeamLogo,
                                           AwayTeamScore = match.AwayTeamScore,
                                           StartDate = match.StartDate,
                                           Tournament = match.Tournament,
                                           Prediction = match.Predictions
                                                                .Select(prediction => new PredictionViewModel
                                                                {
                                                                    Id=prediction.Id,
                                                                    AwayTeamPredict = prediction.AwayTeamPredict,
                                                                    HomeTeamPredict = prediction.HomeTeamPredict
                                                                }).FirstOrDefault()
                                       }).FirstOrDefault();
    }
}