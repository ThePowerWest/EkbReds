using ApplicationCore.Entities.Main;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface ITournamentRepository
    {
        Task<Tournament> FindByNameAsync(string name);
    }
}