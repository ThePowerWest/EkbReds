using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Main;
using Web.Interfaces;

namespace Web.Services
{
    /// <summary>
    /// Сервис для получения лучших игроков
    /// </summary>
    public class BestPlayersService : IBestPlayersService
    {
        /// <summary>
        /// Получить сумму всех баллов за все туры
        /// </summary>
        /// <param name="users"> Пользователи </param>
        /// <returns> Сумма очков </returns>
        public Dictionary<User, int> GetSumPointsForAllTours(IEnumerable<User> users, int seasonId)
        {
            Dictionary<User, int> bestPlayers = new Dictionary<User, int>();
            foreach (User user in users)
            {
                int summ = 0;

                try
                {
                    foreach (Prediction predict in user.Predictions.Where(predict => predict.Match.Tournament.Season.Id == seasonId))
                    {
                        if (predict.Match.HomeTeamScore.HasValue && predict.Match.AwayTeamScore.HasValue)
                        {
                            byte end1 = predict.Match.HomeTeamScore.Value;
                            byte end2 = predict.Match.AwayTeamScore.Value;
                            byte res1 = predict.HomeTeamPredict;
                            byte res2 = predict.AwayTeamPredict;

                            if (end1 == res1 && end2 == res2)
                            {
                                summ += 3;
                            }
                            else if ((end1 > end2 && res1 > res2 && end1 - end2 == res1 - res2) ||
                                     (end1 < end2 && res1 < res2 && end2 - end1 == res2 - res1) ||
                                     (end1 == end2 && res1 == res2))
                            {
                                summ += 2;
                            }
                            else if ((end1 > end2 && res1 > res2) ||
                                     (end1 < end2 && res1 < res2))
                            {
                                summ += 1;
                            }
                        }
                    }
                }
                catch { }
                bestPlayers.Add(user, summ);
                bestPlayers.OrderByDescending(summ => summ);
            }
            return bestPlayers;
        }
    }
}