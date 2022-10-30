using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities.Main
{
    /// <summary>
    /// Статусы матчей
    /// </summary>
    public class MatchStatus
    {
        /// <summary>
        /// Идентификатор статуса матча
        /// </summary>
        [Key]
        public MatchStatusId Id { get; set; }

        /// <summary>
        /// Наименование статуса матча
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }   
}