using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities.Main
{
    /// <summary>
    /// Матч
    /// </summary>
    public class Match : BaseEntity
    {
        ///// <summary>
        ///// Это день или несколько дней, 
        ///// в течение которых все команды определенной лиги
        ///// играют один матч (дома или на выезде), разыгрываемый по жребию
        ///// </summary>
        //public virtual Tour Tour { get; set; }

        /// <summary>
        /// Название домашней команды
        /// </summary>
        public string HomeTeam { get; set; }

        /// <summary>
        /// Логотип домашней команды
        /// </summary>
        public string LogoHomeTeam { get; set; }

        /// <summary>
        /// Название выездной команды
        /// </summary>
        public string AwayTeam { get; set; }

        /// <summary>
        /// Логотип выездной команды
        /// </summary>
        public string LogoAwayTeam { get; set; }

        /// <summary>
        /// Коэффицент домашней команды
        /// </summary>
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OutcomeHomeTeam { get; set; }

        /// <summary>
        /// Коэффицент на ничью
        /// </summary>
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OutcomeDraw { get; set; }

        /// <summary>
        /// Коэффицент команды на выезде
        /// </summary>
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OutcomeAwayTeam { get; set; }

        /// <summary>
        /// Дата начала матча
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// Список прогнозов на матч
        /// </summary>
        public virtual IEnumerable<Prediction> Predictions { get; set; }
    }
}