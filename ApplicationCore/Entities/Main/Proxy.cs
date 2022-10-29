using ApplicationCore.Entities.Base;

namespace ApplicationCore.Entities.Main
{
    /// <summary>
    /// Прокси
    /// </summary>
    public class Proxy : BaseEntity
    {
        /// <summary>
        /// IP адрес
        /// </summary>
        public string IP { get; set; } = string.Empty;

        /// <summary>
        /// Порт
        /// </summary>
        public short Port { get; set; }

        /// <summary>
        /// Логин для авторизации
        /// </summary>
        public string Login { get; set; } = string.Empty;

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}