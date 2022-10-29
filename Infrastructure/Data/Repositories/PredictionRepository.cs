using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Models;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Репозиторий сущности Прогнозы
    /// </summary>
    public class PredictionRepository : 
        EFRepository<ApplicationCore.Entities.Main.Prediction>, IReadRepository<ApplicationCore.Entities.Main.Prediction>, 
        IRepository<ApplicationCore.Entities.Main.Prediction>, IPredictionRepository
    {
        /// <summary>
        /// ctor
        /// </summary>
        public PredictionRepository(MainContext context) : base(context)
        { }

        /// <inheritdoc />
        public async Task UpdatePointAsync(int id, byte point) =>
            await Context.Database.ExecuteSqlRawAsync($"UPDATE Predictions SET Point={point} WHERE Id={id}");

        /// <inheritdoc />
        public async Task<IEnumerable<TopUsers>> TopUsersAsync(byte userNumbers) =>
            await Context.RawSqlQueryAsync(
                $@"SELECT TOP ({userNumbers})
	                   AspNetUsers.SurName + ' ' + AspNetUsers.FirstName as FIO, 
	                   SUM(Point) as Point
                   FROM Predictions
                   LEFT JOIN AspNetUsers ON AspNetUsers.Id = Predictions.UserId
                   LEFT JOIN Matches ON Matches.Id = Predictions.MatchId
                   LEFT JOIN Tournaments ON Tournaments.Id = Matches.TournamentId
                   WHERE Tournaments.SeasonId = (SELECT TOP(1) Id
							                     FROM Seasons
							                     ORDER BY Id DESC)
                   GROUP BY AspNetUsers.SurName + ' ' + AspNetUsers.FirstName
                   ORDER BY Point DESC", 
                table => new TopUsers 
                {
                    FIO = (string)table[0], 
                    Point = (int)table[1] 
                });

        /// <inheritdoc />
        public async Task<IEnumerable<PointsPerMonth>> PointsPerMonthAsync(int seasonId, int month) =>
             await Context.RawSqlQueryAsync(
                $@"SELECT AspNetUsers.SurName + ' ' + AspNetUsers.FirstName AS FIO
                         ,COUNT(CASE WHEN Point=5 THEN 5 END) AS Five
                         ,COUNT(CASE WHEN Point=3 THEN 3 END) AS Three
                         ,COUNT(CASE WHEN Point=2 THEN 2 END) AS Two
                   	     ,COUNT(CASE WHEN Point=1 THEN 1 END) AS One
                   	     ,SUM(Point) as Point
                   FROM Predictions 
                   LEFT JOIN AspNetUsers ON AspNetUsers.Id = Predictions.UserId
                   LEFT JOIN Matches ON Matches.Id = Predictions.MatchId
                   LEFT JOIN Tournaments ON Tournaments.Id = Matches.TournamentId
                   WHERE Tournaments.SeasonId = {seasonId} AND MONTH(Matches.StartDate) = {month}
                   GROUP BY AspNetUsers.SurName + ' ' + AspNetUsers.FirstName
                   ORDER BY Point DESC",
                table => new PointsPerMonth
                {
                    FIO = (string)table[0],
                    RightScore = (int)table[1],
                    GoalDifferenceOrDraw = (int)table[2],
                    MatchOutcome = (int)table[3],
                    UnitedGoals = (int)table[4],
                    Sum = (int)table[5]
                });

        /// <inheritdoc />
        public async Task CreateOrUpdateAsync(string userId, byte homeTeamPredict, byte awayTeamPredict) =>
            await Context.Database.ExecuteSqlRawAsync(
                $@"DECLARE @nextMatch TABLE(
                     startDate datetime2(7) NOT NULL,
                     predictionId int NULL,
                     matchId int NOT NULL);
                
                   INSERT INTO @nextMatch (startDate, PredictionId, matchId)
                   SELECT TOP(1) StartDate,
                                 Predictions.Id,
                                 Matches.Id
                   FROM Matches
                   LEFT JOIN Predictions ON Predictions.MatchId = Matches.Id AND UserId = '{userId}'
                   WHERE DATEADD(HOUR, -5, GETDATE()) < StartDate AND MatchStatusId = 0
                   ORDER BY StartDate
                
                   IF DATEADD(HOUR, 2, GETDATE()) < (SELECT DATEADD(HOUR, 3, startDate) FROM @nextMatch)
                       IF (SELECT PredictionId FROM @nextMatch) IS NULL
                           BEGIN
                               INSERT INTO Predictions (UserId, MatchId, HomeTeamPredict, AwayTeamPredict, CreateDate)
                               VALUES ('{userId}', (SELECT matchId FROM @nextMatch), {homeTeamPredict}, {awayTeamPredict}, GETDATE())
                           END
                       ELSE
                           BEGIN
                               UPDATE Predictions
                               SET AwayTeamPredict = {awayTeamPredict},
                                   HomeTeamPredict = {homeTeamPredict},
                                   CreateDate = GETDATE()
                               WHERE Predictions.Id = (SELECT PredictionId FROM @nextMatch)
                           END");
    }
}