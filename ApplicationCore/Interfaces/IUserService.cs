namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// Сервис для работы с пользователем
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Добавить роль пользователю
        /// </summary>
        Task AddToRoleAsync(string userId, string roleName);
    }
}