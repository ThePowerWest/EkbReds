using ApplicationCore.Entities.Main;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface ISeasonRepository
    {
        Task<Season> LastAsync();
    }
}