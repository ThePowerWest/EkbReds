using ApplicationCore.Entities.Identity;

namespace Web.Models
{
    public class PointTable
    {
        public PointTable(User user)
        {
            User = user;
        }

        public User User { get; set; }

        public byte CorrectScore { get; set; }

        public byte GoalDifference { get; set; }

        public byte TeamVictory { get; set; }

        public byte UnitedScores { get; set; }

        public byte Sum { get; set; }
    }
}