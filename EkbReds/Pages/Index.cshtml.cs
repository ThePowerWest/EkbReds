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
        private readonly IRepository<Prediction> PredictionCRUDRepository;
        private readonly ISeasonRepository SeasonRepository;
        public IEnumerable<Match> Matches;

        [BindProperty]
        public Predict Predict { get; set; }

        public bool IsPredictMaded { get; set; }

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

        /// <summary>
        /// ctor
        /// </summary>
        public IndexModel(
            ISeasonRepository seasonRepository,
            IRepository<Prediction> predictionCRUDRepository,
            UserManager<User> userManager)
        {
            SeasonRepository = seasonRepository;
            PredictionCRUDRepository = predictionCRUDRepository;
            UserManager = userManager;
        }

        public async Task OnGet()
        {
            Season currentSeason = await SeasonRepository.LastAsync();
            Matches = currentSeason.Tournaments.SelectMany(tournament => tournament.Matches);
        }

        ///// <summary>
        ///// Создать прогноз на выбранный матч 
        ///// </summary>
        //public async Task<IActionResult> OnPostMakePredictAsync(int matchId, string username)
        //{
        //    Season currentSeason = await SeasonRepository.LastAsync();
        //    IEnumerable<Match> matches = currentSeason.Tournaments.First().Matches;

        //    Match match = matches.Where(match => match.Id == matchId).First();
        //    User user = await UserManager.FindByNameAsync(username);

        //    await PredictionCRUDRepository.AddAsync(
        //        new Prediction
        //        {
        //            Match = match,
        //            User = user,
        //            HomeTeamPredict = Predict.HomeTeamPredict,
        //            AwayTeamPredict = Predict.AwayTeamPredict
        //        });
        //    return LocalRedirect(Url.Content("~/"));
        //}
    }
}