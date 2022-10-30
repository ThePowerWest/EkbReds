namespace ApplicationCore.Models.SportScore.Objects
{
    /// <summary>
    /// Команда
    /// </summary>
    public class Team
    {
        /// <summary>
        /// Название команды
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Ссылка на логотип команды
        /// </summary>
        public string Logo { get; set; } = string.Empty;
    }
}