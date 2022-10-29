using ApplicationCore.Entities.Identity;

namespace ApplicationCore.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс репозитория сущности Пользователь
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Получить список случайных авторизованных пользователей
        /// </summary>
        /// <param name="number">Число пользователей которых нужно получить</param>
        /// <returns>Список пользователей</returns>
        Task<IEnumerable<User>> RandomUsersAsync(byte number);
    }
}