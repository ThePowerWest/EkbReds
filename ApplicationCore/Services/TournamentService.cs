using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using SportScoreTournament = ApplicationCore.Models.SportScore.Teams.Tournament;

namespace ApplicationCore.Services
{
    /// <summary>
    /// Реализация сервиса для работы с Турнирами
    /// </summary>
    public class TournamentService : ITournamentService
    {
        private readonly ISeasonRepository _seasonRepository;
        private readonly ISportScoreService _sportScoreService;
        private readonly IRepository<Tournament> _tournamentRepository;

        /// <summary>
        /// ctor
        /// </summary>
        public TournamentService(
            ISeasonRepository seasonRepository, 
            ISportScoreService sportScoreService,
            IRepository<Tournament> tournamentRepository)
        {
            _seasonRepository = seasonRepository;
            _sportScoreService = sportScoreService;
            _tournamentRepository = tournamentRepository;
        }

        /// <inheritdoc />
        public async Task CreateAsync()
        {
            Season currentSeason = await _seasonRepository.CurrentIncTAsync();
            IEnumerable<SportScoreTournament> tournaments = 
                await _sportScoreService.GetTournamentsCurrentSeasonAsync(currentSeason.YearEnd);

            if (currentSeason.Tournaments == null)
            {
                await _tournamentRepository.AddRangeAsync(
                    tournaments.Select(tournament =>
                        new Tournament
                        {
                            Name = tournament.Name,
                            Season = currentSeason
                        }));
            }
            else
            {
                IEnumerable<string> newTournaments = tournaments.Select(tournament => tournament.Name)
                    .Except(currentSeason.Tournaments.Select(tournament => tournament.Name));

                foreach (string tournamentName in newTournaments)
                {
                    SportScoreTournament newTournament = tournaments.First(tournament => tournament.Name == tournamentName);

                    await _tournamentRepository.AddAsync(
                        new Tournament
                        {
                            Name = newTournament.Name,
                            Season = currentSeason,
                        });
                }
            }
        }
    }
}