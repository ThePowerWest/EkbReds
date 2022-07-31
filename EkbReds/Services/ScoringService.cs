using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Main;
using Web.Interfaces;
using Web.Models;

namespace Web.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ScoringService : IScoringService
    {
        const string manchesterUnitedName = "Manchester United";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="users"></param>
        /// <param name="season"></param>
        public IEnumerable<PointTable> TopPredictionsByUsers(IEnumerable<User> users, Season season)
        {
            List<PointTable> points = new List<PointTable>();
            foreach (User user in users)
            {
                byte summ = 0;
                if (user.Predictions != null)
                {
                    foreach (Prediction predict in user.Predictions.Where(predict => predict.Match.Tournament.Season == season))
                    {
                        if (predict.Match.HomeTeamScore.HasValue && predict.Match.AwayTeamScore.HasValue)
                        {
                            // Точно угаданный счёт
                            if (predict.Match.HomeTeamScore.Value == predict.HomeTeamPredict &&
                                predict.Match.AwayTeamScore.Value == predict.AwayTeamPredict)
                            {
                                summ += 5;
                            }
                            else if ((predict.Match.HomeTeamScore.Value > predict.Match.AwayTeamScore.Value && predict.HomeTeamPredict > predict.AwayTeamPredict && predict.Match.HomeTeamScore.Value - predict.Match.AwayTeamScore.Value == predict.HomeTeamPredict - predict.AwayTeamPredict) ||
                                     (predict.Match.HomeTeamScore.Value < predict.Match.AwayTeamScore.Value && predict.HomeTeamPredict < predict.AwayTeamPredict && predict.Match.AwayTeamScore.Value - predict.Match.HomeTeamScore.Value == predict.AwayTeamPredict - predict.HomeTeamPredict) ||
                                     (predict.Match.HomeTeamScore.Value == predict.Match.AwayTeamScore.Value && predict.HomeTeamPredict == predict.AwayTeamPredict))
                            {
                                summ += 3;
                            }
                            // Угадан исход матча
                            else if ((predict.Match.HomeTeamScore.Value > predict.Match.AwayTeamScore.Value && predict.HomeTeamPredict > predict.AwayTeamPredict) ||
                                    (predict.Match.HomeTeamScore.Value < predict.Match.AwayTeamScore.Value && predict.HomeTeamPredict < predict.AwayTeamPredict))
                            {
                                summ += 2;
                            }
                            // Угаданы забитые голы только клуба Manchester United
                            else if ((predict.Match.AwayTeamName == manchesterUnitedName && predict.AwayTeamPredict == predict.Match.AwayTeamScore.Value) ||
                                    (predict.Match.HomeTeamName == manchesterUnitedName && predict.HomeTeamPredict == predict.Match.HomeTeamScore.Value))
                            {
                                summ += 1;
                            }
                        }
                    }
                }
                points.Add(new PointTable { User = user, Points = summ });
            }

            return points;
        }        
    }
}