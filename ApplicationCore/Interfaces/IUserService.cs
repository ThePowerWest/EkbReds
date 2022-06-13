namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// Настройки пользователя
    /// </summary>
    public interface IUserService
    {
        Task AddToRoleAsync(string userId, string roleName);

        Task RemoveFromRoleAsync(string userId, string roleName);
    }
}