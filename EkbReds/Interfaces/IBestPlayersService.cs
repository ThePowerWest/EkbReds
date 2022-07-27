using ApplicationCore.Entities.Identity;

namespace Web.Interfaces
{
    public interface IBestPlayersService
    {
        Dictionary<User, int> GetSumPointsForAllTours(IEnumerable<User> users, int seasonId);
    }
}