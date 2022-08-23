namespace Web.Models
{
    public class PointTable
    {
        public PointTable(string firstname, string surname)
        {
            User = new UserViewModel
            {
                FirstName = firstname,
                SurName = surname
            };
        }

        public UserViewModel User { get; set; }

        public byte CorrectScore { get; set; }

        public byte GoalDifference { get; set; }

        public byte TeamVictory { get; set; }

        public byte UnitedScores { get; set; }

        public byte Sum { get; set; }
    }
}