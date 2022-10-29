using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace Web.Pages
{
    /// <summary>
    /// �������� ������������ ���� �� ������ � ������
    /// </summary>
    public class PointsModel : PageModel
    {
        private readonly IReadRepository<Season> _seasonReadRepository;
        private readonly IPredictionRepository _predictionRepository;
        private readonly ISeasonRepository _seasonRepository;
        private readonly UserManagerEx _userManager;

        public List<SelectListItem> Seasons = new();

        /// <summary>
        /// ctor
        /// </summary>
        public PointsModel(
            IReadRepository<Season> seasonReadRepository,
            ISeasonRepository seasonRepository,
            UserManagerEx userManager,
            IPredictionRepository predictionRepository)
        {
            _seasonReadRepository = seasonReadRepository;
            _seasonRepository = seasonRepository;
            _userManager = userManager;
            _predictionRepository = predictionRepository;
        }

        /// <summary>
        /// ���������� �������� �������� � ������
        /// </summary>
        public async Task OnGet()
        {
            await SetSeasons();
        }

        /// <summary>
        /// �������� � ���������� ������ ������� � ���� ������
        /// </summary>
        /// <param name="seasonId">������������� ������</param>
        public async Task<IActionResult> OnPostGetMonths(int seasonId)
        {
            IEnumerable<string> months = await _seasonRepository.GetMonthsAsync(seasonId);
            return new JsonResult(months);
        }

        /// <summary>
        /// ������������ ������� �� ������� �����
        /// </summary>
        /// <param name="seasonId">������������� ������</param>
        /// <param name="month">������������ ������</param>
        public async Task<IActionResult> OnPostGetTable(int seasonId, string month) =>
            new JsonResult(
                await _predictionRepository.PointsPerMonthAsync(
                    seasonId, 
                    DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month));

        #region Private Region
        /// <summary>
        /// �������� � ���������� ������ ������� ��� ������
        /// </summary>
        private async Task SetSeasons()
        {
            foreach(Season season in await _seasonReadRepository.ListAsync())
            {
                Seasons.Add(new SelectListItem
                {
                    Value = season.Id.ToString(),
                    Text = $"{season.YearStart}/{season.YearEnd} �����"
                });
            }
            Seasons.OrderByDescending(season => season).First().Selected = true;
        }
        #endregion
    }
}