using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;

namespace ApplicationCore.Services
{
    /// <summary>
    /// Реализация сервиса подсчёта очков за проставленные прогнозы
    /// </summary>
    public class ScoreService : IScoreService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IRepository<Prediction> _predictionRepository;

        /// <summary>
        /// ctor
        /// </summary>
        public ScoreService(
            IMatchRepository matchRepository,
            IRepository<Prediction> predictionRepository
            )
        {
            _matchRepository = matchRepository;
            _predictionRepository = predictionRepository;
        }

        /// <inheritdoc />
        public async Task RecalculatePoints()
        {
            IEnumerable<Match> matches = await _matchRepository.PastAsync();

            foreach (Match match in matches)
            {
                foreach (Prediction prediciton in match.Predictions)
                {
                    byte point = GetPointForPrediction(prediciton.HomeTeamPredict, prediciton.AwayTeamPredict,
                                (byte)match.HomeTeamScore, (byte)match.AwayTeamScore, match.HomeTeamName, match.AwayTeamName);
                    prediciton.Point = point;
                    await _predictionRepository.UpdateAsync(prediciton);
                }
            }
        }

        /// <summary>
        /// Расчитать количество очков за прогноз
        /// </summary>
        /// <param name="predictionHome">Прогноз на домашнюю команду</param>
        /// <param name="predictionAway">Прогноз на гостевую команду</param>
        /// <param name="finalHomeScore">Конечный счёт в матче домашней команды</param>
        /// <param name="finalAwayScore">Конечный счёт в матче гостевой команды</param>
        /// <param name="homeTeamName">Название домашней команды</param>
        /// <param name="awayTeamName">Название гостевой команды</param>
        /// <returns>Количество очков за проставленный прогноз</returns>
        private byte GetPointForPrediction(byte predictionHome, byte predictionAway, 
            byte finalHomeScore, byte finalAwayScore, string homeTeamName, string awayTeamName)
        {
            const string manchesterUnitedName = "Manchester United";

            // Точно угаданный счёт
            if (predictionHome == finalHomeScore &&
                predictionAway == finalAwayScore)
            {
                return 5;
            }
            // Верно спрогнозированный исход и разница мячей или ничья(любая ничья)
            else if ((finalHomeScore > finalAwayScore && predictionHome > predictionAway && finalHomeScore - finalAwayScore == predictionHome - predictionAway) ||
                     (finalHomeScore < finalAwayScore && predictionHome < predictionAway && finalAwayScore - finalHomeScore == predictionAway - predictionHome) ||
                     (finalHomeScore == finalAwayScore && predictionHome == predictionAway))
            {
                return 3;
            }
            // Угадан исход матча(победа той или иной команды)
            else if ((finalHomeScore > finalAwayScore && predictionHome > predictionAway) ||
                     (finalHomeScore < finalAwayScore && predictionHome < predictionAway))
            {
                return 2;
            }
            // Угаданы забитые голы только клуба Manchester United
            else if ((awayTeamName == manchesterUnitedName && predictionAway == finalAwayScore) ||
                     (homeTeamName == manchesterUnitedName && predictionHome == finalHomeScore))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}