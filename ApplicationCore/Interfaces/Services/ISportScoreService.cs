namespace ApplicationCore.Interfaces.Services
{
    public interface ISportScoreService
    {
        Task UpdateSeason();

        Task UpdateTournaments();

        Task UpdateMatches();
    }
}