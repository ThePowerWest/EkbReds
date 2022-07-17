using ApplicationCore.Models;

namespace ApplicationCore.Interfaces.SportScore
{
    public interface IMUMatches
    {
        Task GetNextGame();
    }
}