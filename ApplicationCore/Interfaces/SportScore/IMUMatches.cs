using ApplicationCore.Models;
using ApplicationCore.Models.SportScore.Teams;

namespace ApplicationCore.Interfaces.SportScore
{
    public interface IMUMatches
    {
        Task<List<EventData>> GetNextGames();
    }
}