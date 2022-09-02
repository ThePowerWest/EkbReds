using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using Web.Models;

namespace Web.Pages
{
    public class PointsModel : PageModel
    {
        private readonly IReadRepository<Season> SeasonReadRepository;
        private readonly ISeasonRepository SeasonRepository;
        private readonly UserManagerEx UserManager;

        public List<SelectListItem> Seasons = new();

        public PointsModel(
            IReadRepository<Season> seasonReadRepository,
            ISeasonRepository seasonRepository,
            UserManagerEx userManager)
        {
            SeasonReadRepository = seasonReadRepository;
            SeasonRepository = seasonRepository;
            UserManager = userManager;
        }

        public async Task OnGet()
        {
            await GetSeasons();
        }

        private async Task GetSeasons()
        {
            IEnumerable<Season> seasons = await SeasonReadRepository.ListAsync();
            for (int i = 0; i < seasons.Count(); i++)
            {
                Season selectSeason = seasons.ElementAt(i);
                Seasons.Add(new SelectListItem
                {
                    Value = selectSeason.Id.ToString(),
                    Text = $"{selectSeason.YearStart}/{selectSeason.YearEnd} сезон",
                    Selected = i == seasons.Count() - 1
                });
            }
        }

        public async Task<IActionResult> OnPostGetMonths(int seasonId)
        {
            IEnumerable<string> months = await SeasonRepository.GetMonths(seasonId);
            return new JsonResult(months);
        }

        public async Task<IActionResult> OnPostGetTable(int seasonId, string month)
        {
            IEnumerable<User> users = await UserManager.FindUsersWithCurrentSeasonPaidAsync();
            List<PointTable> points = new();

            foreach (User user in users)
            {
                PointTable point = new(user.FirstName, user.SurName);

                if (user.Predictions != null)
                {
                    foreach (Prediction predict in user.Predictions.Where(predict =>
                        predict.Match.StartDate.Month == DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month &&
                        predict.Match.Tournament.Season.Id == seasonId))
                    {
                        if (predict.Match.HomeTeamScore.HasValue && predict.Match.AwayTeamScore.HasValue)
                        {
                            // “очно угаданный счЄт
                            if (predict.Match.HomeTeamScore.Value == predict.HomeTeamPredict &&
                                predict.Match.AwayTeamScore.Value == predict.AwayTeamPredict)
                            {
                                point.Sum += 5;
                                point.CorrectScore += 1;
                            }
                            // »ли ничь€ или верно спрогнозированный исход и разница м€чей
                            else if ((predict.Match.HomeTeamScore.Value > predict.Match.AwayTeamScore.Value && predict.HomeTeamPredict > predict.AwayTeamPredict && predict.Match.HomeTeamScore.Value - predict.Match.AwayTeamScore.Value == predict.HomeTeamPredict - predict.AwayTeamPredict) ||
                                     (predict.Match.HomeTeamScore.Value < predict.Match.AwayTeamScore.Value && predict.HomeTeamPredict < predict.AwayTeamPredict && predict.Match.AwayTeamScore.Value - predict.Match.HomeTeamScore.Value == predict.AwayTeamPredict - predict.HomeTeamPredict) ||
                                     (predict.Match.HomeTeamScore.Value == predict.Match.AwayTeamScore.Value && predict.HomeTeamPredict == predict.AwayTeamPredict))
                            {
                                point.Sum += 3;
                                point.GoalDifference += 1;
                            }
                            // ”гадан исход матча
                            else if ((predict.Match.HomeTeamScore.Value > predict.Match.AwayTeamScore.Value && predict.HomeTeamPredict > predict.AwayTeamPredict) ||
                                    (predict.Match.HomeTeamScore.Value < predict.Match.AwayTeamScore.Value && predict.HomeTeamPredict < predict.AwayTeamPredict))
                            {
                                point.Sum += 2;
                                point.TeamVictory += 1;
                            }
                            // ”гаданы забитые голы только клуба Manchester United
                            else if ((predict.Match.AwayTeamName == "Manchester United" && predict.AwayTeamPredict == predict.Match.AwayTeamScore.Value) ||
                                    (predict.Match.HomeTeamName == "Manchester United" && predict.HomeTeamPredict == predict.Match.HomeTeamScore.Value))
                            {
                                point.Sum += 1;
                                point.UnitedScores += 1;
                            }
                        }
                    }
                }
                points.Add(point);
            }

            return new JsonResult(points.OrderByDescending(point => point.Sum));
        }
    }
}