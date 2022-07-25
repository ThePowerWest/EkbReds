using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace EkbReds.Pages
{
    /// <summary>
    /// Главная страница для прогнозов
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> UserManager;
        IRepository<Prediction> PredictionCRUDRepository;
        IReadRepository<Prediction> PredictionRepository;
        ISeasonRepository SeasonRepository;
        public IEnumerable<Match> Matches;
        public Prediction Prediction;

        /// <summary>
        /// Элемент передачи данных со страницы
        /// </summary>
        [BindProperty]
        public Predict Predict { get; set; }
        public bool IsPredictMaded { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public IndexModel(ISeasonRepository seasonRepository,
            IRepository<Prediction> predictionCRUDRepository,
            UserManager<User> userManager,
            IReadRepository<Prediction> predictionRepository)
        {
            SeasonRepository = seasonRepository;
            PredictionCRUDRepository = predictionCRUDRepository;
            UserManager = userManager;
            PredictionRepository = predictionRepository;
        }

        public async Task OnGet()
        {
            Season currentSeason = await SeasonRepository.LastAsync();
            Matches = currentSeason.Tournaments.First().Matches;

            //// Все прогнозы
            //var predictions = await PredictionRepository.ListAsync();
            //// Прогнозы на первый матч
            //var currentMatchPredictions = predictions.Where(p => p.Match.Id == Matches.First().Id);
            //// Пользователи, сделавшие прогноз
            //var predictMadedUsers = currentMatchPredictions.Select(p => p.User.UserName).ToList();
            //try
            //{
            //    if (predictMadedUsers.Contains(User.Identity.Name))
            //    {
            //        // Сделанный прогноз
            //        Prediction = currentMatchPredictions.Where(p => p.User.UserName == User.Identity.Name).First();
            //        IsPredictMaded = true;
            //    }
            //}
            //catch { }
        }

        /// <summary>
        /// Создает прогноз в бд
        /// </summary>
        public async Task<IActionResult> OnPostMakePredictAsync(int matchId, string username)
        {
            Season currentSeason = await SeasonRepository.LastAsync();
            var matches = currentSeason.Tournaments.First().Matches;

            Match match = matches.Where(match => match.Id == matchId).First();
            User user = await UserManager.FindByNameAsync(username);
            Prediction predict = new Prediction
            {
                Match = match,
                User = user,
                HomeTeamPredict = Predict.HomeTeamPredict,
                AwayTeamPredict = Predict.AwayTeamPredict
            };
            await PredictionCRUDRepository.AddAsync(predict);
            return LocalRedirect(Url.Content("~/"));
        }
    }

    /// <summary>
    /// Модель ввода данных со страницы
    /// </summary>
    public class Predict
    {
        /// <summary>
        /// Счет домашней команды
        /// </summary>
        [Required(ErrorMessage = "Поле обязательно!")]
        public byte HomeTeamPredict { get; set; }

        /// <summary>
        /// Счет команды на выезде
        /// </summary>
        [Required(ErrorMessage = "Поле обязательно!")]
        public byte AwayTeamPredict { get; set; }
    }

}