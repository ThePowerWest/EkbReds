using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EkbReds.Pages
{
    /// <summary>
    /// Главная страница для прогнозов
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> UserManager;
        private readonly IRepository<Prediction> PredictionCRUDRepository;
        private readonly ISeasonRepository SeasonRepository;
        private readonly IMatchRepository MatchRepository;
        private readonly IRepository<Match> MatchCRUDRepository;
        private readonly IPredictionRepository PredictionRepository;

        public IList<MatchViewModel> Matches = new List<MatchViewModel>();

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
            /// Список прогнозов
            /// </summary>
            public Prediction Prediction { get; set; }
        }

        /// <summary>
        /// ctor
        /// </summary>
        public IndexModel(
            ISeasonRepository seasonRepository,
            IRepository<Prediction> predictionCRUDRepository,
            UserManager<User> userManager,
            IMatchRepository matchRepository,
            IPredictionRepository predictionRepository,
            IRepository<Match> matchCRUDRepository)
        {
            SeasonRepository = seasonRepository;
            PredictionCRUDRepository = predictionCRUDRepository;
            UserManager = userManager;
            MatchRepository = matchRepository;
            PredictionRepository = predictionRepository;
            MatchCRUDRepository = matchCRUDRepository;
        }

        /// <summary>
        /// Отобразить страницу с прогнозами
        /// </summary>
        /// <returns></returns>
        public async Task OnGet()
        {
            IEnumerable<Match> matches = MatchRepository.Next(4);
            
            for(int countMatch = 0; countMatch < matches.Count(); countMatch++)
            {
                Matches.Add(new MatchViewModel
                {
                    Id = matches.ElementAt(countMatch).Id,
                    HomeTeamName = matches.ElementAt(countMatch).HomeTeamName,
                    HomeTeamLogo = matches.ElementAt(countMatch).HomeTeamLogo,
                    AwayTeamName = matches.ElementAt(countMatch).AwayTeamName,
                    AwayTeamLogo = matches.ElementAt(countMatch).AwayTeamLogo,
                    StartDate = matches.ElementAt(countMatch).StartDate,
                    Tournament = matches.ElementAt(countMatch).Tournament,
                    Prediction = countMatch == 0 ? await PredictionRepository.FirstOrDefaultAsync(
                                                            matches.ElementAt(countMatch),
                                                            await UserManager.GetUserAsync(User)) : null

            });
            }
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