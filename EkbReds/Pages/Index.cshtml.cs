using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;

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
        private readonly IBestPlayersService BestPlayersService;

        public IList<MatchViewModel> Matches = new List<MatchViewModel>();
        public Dictionary<User, int> BestUsers;

        /// <summary>
        /// Модель отображения данных 
        /// на View для сущности Матча
        /// </summary>
        public class MatchViewModel
        {
            /// <summary>
            /// Идентификатор матча
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Название домашней команды
            /// </summary>
            public string HomeTeamName { get; set; }

            /// <summary>
            /// Ссылка на эмблему домашней команды
            /// </summary>
            public string HomeTeamLogo { get; set; }

            /// <summary>
            /// Название выездной команды
            /// </summary>
            public string AwayTeamName { get; set; }

            /// <summary>
            /// Ссылка на эмблему выездной команды
            /// </summary>
            public string AwayTeamLogo { get; set; }

            /// <summary>
            /// Дата и время начала матча
            /// </summary>
            public DateTime StartDate { get; set; }

            /// <summary>
            /// Турнир, по которому идет матч
            /// </summary>
            public Tournament Tournament { get; set; }

            /// <summary>
            /// Прогноз на матч
            /// </summary>
            public PredictionViewModel? Prediction { get; set; }
        }

        /// <summary>
        /// Модель отображения данных 
        /// на View для сущности Прогноз
        /// </summary>
        public class PredictionViewModel
        {
            /// <summary>
            /// Идентификатор прогноза
            /// </summary>  
            public int Id { get; set; }

            /// <summary>
            /// Пользователь который проставил прогноз
            /// </summary>
            public User User { get; set; }

            /// <summary>
            /// Матч на который проставлен прогноз
            /// </summary>
            public Match Match { get; set; }

            /// <summary>
            /// Счёт на домашнюю команду
            /// </summary>
            public byte HomeTeamPredict { get; set; }

            /// <summary>
            /// Счёт на выездную команду
            /// </summary>
            public byte AwayTeamPredict { get; set; }
        }


        /// <summary>
        /// ctor
        /// </summary>
        public IndexModel(
            IRepository<Prediction> predictionCRUDRepository,
            UserManager<User> userManager,
            IMatchRepository matchRepository,
            IPredictionRepository predictionRepository,
            IRepository<Match> matchCRUDRepository,
            IBestPlayersService bestPlayersService)
        {
            PredictionCRUDRepository = predictionCRUDRepository;
            UserManager = userManager;
            MatchRepository = matchRepository;
            PredictionRepository = predictionRepository;
            MatchCRUDRepository = matchCRUDRepository;
            BestPlayersService = bestPlayersService;
        }

        /// <summary>
        /// Отобразить страницу с прогнозами
        /// </summary>
        /// <returns></returns>
        public async Task OnGet()
        {
            MatchRepository.Currents();

            IEnumerable<Match> matches = MatchRepository.Next(4);

            for (int countMatch = 0; countMatch < matches.Count(); countMatch++)
            {
                Prediction currentPrediction = countMatch == 0 ? await PredictionRepository
                    .FirstOrDefaultAsync(matches.ElementAt(countMatch), await UserManager.GetUserAsync(User))
                                                               : null;

                Matches.Add(new MatchViewModel
                {
                    Id = matches.ElementAt(countMatch).Id,
                    HomeTeamName = matches.ElementAt(countMatch).HomeTeamName,
                    HomeTeamLogo = matches.ElementAt(countMatch).HomeTeamLogo,
                    AwayTeamName = matches.ElementAt(countMatch).AwayTeamName,
                    AwayTeamLogo = matches.ElementAt(countMatch).AwayTeamLogo,
                    StartDate = matches.ElementAt(countMatch).StartDate,
                    Tournament = matches.ElementAt(countMatch).Tournament,
                    Prediction = countMatch == 0 && currentPrediction != null ?
                            new PredictionViewModel
                            {
                                Id = currentPrediction.Id,
                                HomeTeamPredict = currentPrediction.HomeTeamPredict,
                                AwayTeamPredict = currentPrediction.AwayTeamPredict
                            } : null

                });
            }

            BestUsers = BestPlayersService.GetSumPointsForAllTours(UserManager.Users, matches
                .First().Tournament.Season.Id)
                .Take(10)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Создать прогноз на выбранный матч 
        /// </summary>
        public async Task<IActionResult> OnPostMakePredictionAsync(MatchViewModel match)
        {
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
                Match currentMatch = await MatchCRUDRepository.GetByIdAsync(match.Id);
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
    }
}