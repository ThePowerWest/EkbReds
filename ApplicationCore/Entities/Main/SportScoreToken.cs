namespace ApplicationCore.Entities.Main
{
    /// <summary>
    /// API токен для авторизации на SportScore
    /// </summary>
    public class SportScoreToken : BaseEntity
    {
        /// <summary>
        /// Ключ токена
        /// </summary>
        public string Key { get; set; }
    }
}