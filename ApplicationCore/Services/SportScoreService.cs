using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class SportScoreService : ISportScoreService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public SportScoreService() { }

        public void CheckSeason()
        {
            
        }

        public void CheckNewMatchesAndSeason()
        {
            if (IsNewSeason())
            {
                CreateNewSeason();
            }
            else
            {

            }
        }

        private bool IsNewSeason()
        {
            return false;
        }

        private void CreateNewSeason()
        {

        }
    }
}