using ApplicationCore.Entities.DTO;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для загрузки матчей
    /// </summary>
    public interface IMatchLoadService
    {
        Task<List<Match>> LoadAsync();
    }
}