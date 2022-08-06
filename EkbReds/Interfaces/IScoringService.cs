using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Main;
using Web.Models;

namespace Web.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IScoringService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="users"></param>
        /// <param name="season"></param>
        /// <returns></returns>
        IEnumerable<PointTopTable> TopPredictionsByUsers(IEnumerable<User> users, Season season);
    }
}