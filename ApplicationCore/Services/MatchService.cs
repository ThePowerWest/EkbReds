using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models.SportScore.Teams;
using Tournament = ApplicationCore.Entities.Main.Tournament;

namespace ApplicationCore.Services
{
    /// <summary>
    /// Сервис для работы с матчами
    /// </summary>
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IRepository<Match> _matchCRUDRepository;
        private readonly ITournamentRepository _tournamentRepository;
        private readonly ISportScoreService _sportScoreService;
        private readonly IScoreService _scoreService;

        /// <summary>
        /// ctor
        /// </summary>
        public MatchService(
            IRepository<Match> matchCRUDRepository,
            IMatchRepository matchRepository,
            ITournamentRepository tournamentRepository,
            ISportScoreService sportScoreService,
            IScoreService scoreService)
        {
            _matchCRUDRepository = matchCRUDRepository;
            _matchRepository = matchRepository;
            _tournamentRepository = tournamentRepository;
            _sportScoreService = sportScoreService;
            _scoreService = scoreService;
        }

        /// <inheritdoc />
        public async Task CreateOrUpdateAsync()
        {
            IEnumerable<Match> notStartedMatches = await _matchRepository.GetNotStartedAsync();
            IEnumerable<EventData> allMatches = await _sportScoreService.AllMatchesAsync();
            IEnumerable<Tournament> currentTournaments = await _tournamentRepository.CurrentsAsync();

            foreach (EventData matchSS in allMatches)
            {
                Match? foundMatch = notStartedMatches.FirstOrDefault(matchDB =>
                                            matchSS.HomeTeam.Name == matchDB.HomeTeamName &&
                                            matchSS.AwayTeam.Name == matchDB.AwayTeamName &&
                                            matchSS.Tournament.Name == matchDB.Tournament.Name);

                if (foundMatch == null)
                {
                    if (matchSS.StartAt > DateTime.Now)
                    {
                        Tournament? tournament = currentTournaments.FirstOrDefault(tournament => tournament.Name == matchSS.Tournament.Name);

                        if (tournament != null)
                        {
                            await _matchCRUDRepository.AddAsync(
                                    new Match
                                    {
                                        HomeTeamName = matchSS.HomeTeam.Name,
                                        HomeTeamLogo = matchSS.HomeTeam.Logo,
                                        AwayTeamName = matchSS.AwayTeam.Name,
                                        AwayTeamLogo = matchSS.AwayTeam.Logo,
                                        StartDate = matchSS.StartAt,
                                        MatchStatusId = MatchStatusId.NotStarted,
                                        Tournament = tournament
                                    });
                        }
                    }
                }
                else
                {
                    if (matchSS.HomeScore != null && matchSS.AwayScore != null)
                    {
                        foundMatch.HomeTeamScore = matchSS.HomeScore.Current;
                        foundMatch.AwayTeamScore = matchSS.AwayScore.Current;
                        foundMatch.MatchStatusId = MatchStatusId.Finished;

                        await _matchCRUDRepository.UpdateAsync(foundMatch);
                    }

                    if (matchSS.Status == "postponed" && foundMatch.MatchStatusId != MatchStatusId.Postponed)
                    {
                        foundMatch.MatchStatusId = MatchStatusId.Postponed;

                        await _matchCRUDRepository.UpdateAsync(foundMatch);
                    }

                    if (matchSS.StartAt != foundMatch.StartDate)
                    {
                        foundMatch.StartDate = matchSS.StartAt;

                        await _matchCRUDRepository.UpdateAsync(foundMatch);
                    }
                }
            }
            await _scoreService.RecalculatePoints();
        }
    }
}