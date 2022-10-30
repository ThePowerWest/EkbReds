namespace ApplicationCore.Entities.Main
{
    /// <summary>
    /// Идентификатор статуса матчей
    /// </summary>
    public enum MatchStatusId : byte
    {
        /// <summary>
        /// Матч ещё не начался
        /// </summary>
        NotStarted,
        
        /// <summary>
        /// Матч закончился
        /// </summary>
        Finished,
        
        /// <summary>
        /// Матч отменён/перенесён на другое время
        /// </summary>
        Postponed
    }
}