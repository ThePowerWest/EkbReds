using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Models.Achievement;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    /// <summary>
    /// Страница с достижениями пользователей
    /// </summary>
    public class AchievementsModel : PageModel
    {
        protected readonly IPredictionRepository _predictionRepository;

        public MostAccurateUser? MostAccurateUser;

        /// <summary>
        /// ctor
        /// </summary>
        public AchievementsModel(IPredictionRepository predictionRepository)
        {
            _predictionRepository = predictionRepository;
        }

        /// <summary>
        /// Отображение страницы
        /// </summary>
        public async Task OnGet()
        {
            MostAccurateUser = await _predictionRepository.MostAccuratePredictionsUser();
        }
    }
}