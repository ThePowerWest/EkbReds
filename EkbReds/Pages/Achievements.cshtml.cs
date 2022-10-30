using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Models.Achievement;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    /// <summary>
    /// �������� � ������������ �������������
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
        /// ����������� ��������
        /// </summary>
        public async Task OnGet()
        {
            MostAccurateUser = await _predictionRepository.MostAccuratePredictionsUser();
        }
    }
}