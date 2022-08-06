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
        public IEnumerable<PointTopTable> TopPredictionsByUsers(IEnumerable<User> users, Season season)
        {
            List<PointTopTable> points = new List<PointTopTable>();
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
                            // Или ничья или верно спрогнозированный исход и разница мячей
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
                points.Add(new PointTopTable { User = user, Points = summ });
            }

            return points;
        }        
    }
}