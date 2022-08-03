using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class PointsModel : PageModel
    {
        private readonly IReadRepository<Season> SeasonReadRepository;

        

        public PointsModel(IReadRepository<Season> seasonReadRepository)
        {
            SeasonReadRepository = seasonReadRepository;
        }

        public void OnGet()
        {
        }

        private async Task GetSeason()
        {
            //IEnumerable<Season> seasons = await SeasonReadRepository.ListAsync();
            //for (int i = 0; i < seasons.Count(); i++)
            //{
            //    Seasons.Add(new SelectListItem
            //    {
            //        Value = seasons.ElementAt(i).Id.ToString(),
            //        Text = seasons.ElementAt(i).Name,
            //        Selected = i == seasons.Count() - 1
            //    });
            //}
        }
    }
}
