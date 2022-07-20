using ApplicationCore.Entities.Main;
using ApplicationCore.Models.SportScore.Teams;

namespace ApplicationCore.Interfaces
{
    public interface ISportScoreService
    {
        Task<List<EventData>> GetNextGames();
    }
}