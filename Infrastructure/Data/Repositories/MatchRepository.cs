using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Репозиторий сущности Матч
    /// </summary>
    public class MatchRepository : 
        EFRepository<Match>, IReadRepository<Match>, 
        IRepository<Match>, IMatchRepository
    {
        /// <summary>
        /// ctor
        /// </summary>
        public MatchRepository(MainContext context) : base(context)
        { }

        /// <inheritdoc />
        public async Task<IEnumerable<Match>> PastAsync() => 
            await Context.Matches.Include(match => match.Predictions)
                .Where(match => match.MatchStatusId == MatchStatusId.Finished).ToListAsync();

        /// <inheritdoc />
        public async Task<IEnumerable<Match>> GetNotStartedAsync() => 
            await Context.Matches
            .Include(match => match.Tournament)
                .Where(match => match.MatchStatusId != MatchStatusId.Finished)
                .ToListAsync();

        /// <inheritdoc />
        public async Task<IEnumerable<Match>> CurrentsAsync() =>
            await Context.Matches
                       .Include(match => match.Tournament)
                           .Where(match => match.Tournament.Season == Context.Seasons.OrderByDescending(season => season.YearStart).First())
                           .AsNoTracking().ToListAsync();

        /// <inheritdoc />
        public async Task<NextMatch?> NextAsync(User user) =>
             await Context.Matches.OrderBy(match => match.StartDate)
                                  .Where(match => DateTime.Now.AddHours(-5) < match.StartDate && match.MatchStatusId == MatchStatusId.NotStarted)
                                  .GroupJoin(Context.Predictions.Where(prediction => prediction.User == user),
                                             match => match.Id,
                                             prediction => prediction.Match.Id,
                                          (match, prediction) => new { match, prediction })
                                  .SelectMany(elements => elements.prediction.DefaultIfEmpty(),
                                          (match, prediction) =>
                                   new NextMatch
                                   {
                                       Id = match.match.Id,
                                       HomeTeamLogo = match.match.HomeTeamLogo,
                                       HomeTeamName = match.match.HomeTeamName,
                                       AwayTeamLogo = match.match.AwayTeamLogo,
                                       AwayTeamName = match.match.AwayTeamName,
                                       Tournament = match.match.Tournament,
                                       StartDate = match.match.StartDate,
                                       PredictionEndDate = match.match.StartDate,
                                       Prediction = prediction ?? new Prediction()
                                   }).Take(1).AsNoTracking().FirstOrDefaultAsync();

        /// <inheritdoc />
        public async Task<NextMatch?> NextAsync() =>
            await Context.Matches.OrderBy(match => match.StartDate)
                                 .Where(match => DateTime.Now.AddHours(-5) < match.StartDate && match.MatchStatusId == MatchStatusId.NotStarted)
                                 .Select(match =>
                                         new NextMatch
                                         {
                                             Id = match.Id,
                                             HomeTeamLogo = match.HomeTeamLogo,
                                             HomeTeamName = match.HomeTeamName,
                                             AwayTeamLogo = match.AwayTeamLogo,
                                             AwayTeamName = match.AwayTeamName,
                                             Tournament = match.Tournament,
                                             PredictionEndDate = match.StartDate,
                                             StartDate = match.StartDate,
                                         }).Take(1).AsNoTracking().FirstOrDefaultAsync();

        /// <inheritdoc />
        public async Task<IEnumerable<PreviousMatch>> PreviousAsync(byte number, string userId) =>
            await Context.Matches.Where(match => match.MatchStatusId == MatchStatusId.Finished)
                                         .OrderByDescending(match => match.StartDate)
                                         .Join(Context.Predictions.Where(prediction => prediction.User.Id == userId),
                                                  match => match.Id,
                                                  prediction => prediction.Match.Id,
                                        (match, prediction) =>
                                        new PreviousMatch
                                        {
                                            HomeTeamName = match.HomeTeamName,
                                            HomeTeamScore = match.HomeTeamScore ?? default,
                                            AwayTeamName = match.AwayTeamName,
                                            AwayTeamScore = match.AwayTeamScore ?? default,
                                            Prediction = prediction
                                        }).Take(number).AsNoTracking().ToListAsync();

        /// <inheritdoc />
        public async Task<IEnumerable<PreviousMatch>> PreviousAsync(byte number) =>
            await Context.Matches.Where(match => match.MatchStatusId == MatchStatusId.Finished)
                                 .OrderByDescending(match => match.StartDate)
                                 .Select(match =>
                                         new PreviousMatch
                                         {
                                             HomeTeamName = match.HomeTeamName,
                                             HomeTeamScore = match.HomeTeamScore ?? default,
                                             AwayTeamName = match.AwayTeamName,
                                             AwayTeamScore = match.AwayTeamScore ?? default
                                         }).Take(number).AsNoTracking().ToListAsync();

        /// <inheritdoc />
        public async Task<IEnumerable<FutureMatch>> FutureAsync(byte number) =>
            await Context.Matches.Where(match => DateTime.Now < match.StartDate && match.MatchStatusId == MatchStatusId.NotStarted)
                                 .OrderBy(match => match.StartDate)
                                 .Select(match =>
                                         new FutureMatch
                                         {
                                             HomeTeamName = match.HomeTeamName,
                                             HomeTeamLogo = match.HomeTeamLogo,
                                             AwayTeamName = match.AwayTeamName,
                                             AwayTeamLogo = match.AwayTeamLogo,
                                             StartDate = match.StartDate.AddHours(5),
                                             Tournament = match.Tournament
                                         }).Skip(1).Take(number).AsNoTracking().ToListAsync();
    }
}