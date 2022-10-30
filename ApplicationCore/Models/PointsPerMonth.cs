namespace ApplicationCore.Models
{
    /// <summary>
    /// Количество угаданных очков за каждый из указанных вариантов
    /// </summary>
    public class PointsPerMonth
    {
        /// <summary>
        /// ФИО
        /// </summary>
        public string FIO { get; set; }

        /// <summary>
        /// Полностью правильно угаданный счёт
        /// </summary>
        public int RightScore { get; set; }

        /// <summary>
        /// Угадана разница мячей или ничья
        /// </summary>
        public int GoalDifferenceOrDraw { get; set; }

        /// <summary>
        /// Угадана победившая команда
        /// </summary>
        public int MatchOutcome { get; set; }

        /// <summary>
        /// Угадано количество голов юнайтед
        /// </summary>
        public int UnitedGoals { get; set; }

        /// <summary>
        /// Сумма всех очков
        /// </summary>
        public int Sum { get; set; }
    }
}