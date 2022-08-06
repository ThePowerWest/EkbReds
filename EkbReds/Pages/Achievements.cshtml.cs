using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    /// <summary>
    /// Страница с ачивками
    /// </summary>
    public class AchievementsModel : PageModel
    {
        private readonly UserManager<User> UserManager;
        ISportScoreService SportScoreService;
        IEnumerable<User> Users;

        public UserCountModel MostAccuratePlayer;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="userManager"></param>
        public AchievementsModel(UserManager<User> userManager,
            ISportScoreService sportScoreService)
        {
            UserManager = userManager;
            SportScoreService = sportScoreService;
        }

        public void OnGet()
        {
            Users = UserManager.Users.Where(u => u.UserName != "thepowerwest");

            MostAccuratePlayer = SportScoreService.MostAccuratePlayer(Users);
        }
    }
}
