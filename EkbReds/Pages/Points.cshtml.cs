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
    /// Страница отображающая очки за сезоны и месяцы
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
        /// Отобразить основную страницу с очками
        /// </summary>
        public async Task OnGet()
        {
            await SetSeasons();
        }

        /// <summary>
        /// Получить и установить список месяцев в этом сезоне
        /// </summary>
        /// <param name="seasonId">Идентификатор сезона</param>
        public async Task<IActionResult> OnPostGetMonths(int seasonId)
        {
            IEnumerable<string> months = await _seasonRepository.GetMonthsAsync(seasonId);
            return new JsonResult(months);
        }

        /// <summary>
        /// Сформировать таблицу за текущий месяц
        /// </summary>
        /// <param name="seasonId">Идентификатор сезона</param>
        /// <param name="month">Наименование месяца</param>
        public async Task<IActionResult> OnPostGetTable(int seasonId, string month) =>
            new JsonResult(
                await _predictionRepository.PointsPerMonthAsync(
                    seasonId, 
                    DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month));

        #region Private Region
        /// <summary>
        /// Получить и установить список сезонов для выбора
        /// </summary>
        private async Task SetSeasons()
        {
            foreach(Season season in await _seasonReadRepository.ListAsync())
            {
                Seasons.Add(new SelectListItem
                {
                    Value = season.Id.ToString(),
                    Text = $"{season.YearStart}/{season.YearEnd} сезон"
                });
            }
            Seasons.OrderByDescending(season => season).First().Selected = true;
        }
        #endregion
    }
}