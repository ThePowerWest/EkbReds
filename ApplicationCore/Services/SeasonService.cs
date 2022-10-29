using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using SportScoreTournament = ApplicationCore.Models.SportScore.Teams.Tournament;

namespace ApplicationCore.Services
{
    /// <summary>
    /// Сервис для работы с сезонами
    /// </summary>
    public class SeasonService : ISeasonService
    {
        private readonly ISeasonRepository _seasonRepository;
        private readonly IRepository<Season> _seasonCRUDRepository;
        private readonly ISportScoreService _sportScoreService;

        public SeasonService(
            ISeasonRepository seasonRepository,
            IRepository<Season> seasonCRUDRepository,
            ISportScoreService sportScoreService)
        {
            _seasonRepository = seasonRepository;
            _seasonCRUDRepository = seasonCRUDRepository;
            _sportScoreService = sportScoreService;
        }

        /// <inheritdoc />
        public async Task CreateAsync()
        {
            Season currentSeasonDataBase = await _seasonRepository.CurrentAsync();
            SportScoreTournament currentSeasonSportScore = await _sportScoreService.CurrentSeasonAsync();

            if (currentSeasonDataBase == null || 
                currentSeasonDataBase.YearEnd != currentSeasonSportScore.YearEnd) 
                    await CreateAsync(currentSeasonSportScore);
        }

        /// <summary>
        /// Создать новый сезон
        /// </summary>
        /// <param name="season">Сезон Sport Score</param>
        private async Task CreateAsync(SportScoreTournament season) =>
            await _seasonCRUDRepository.AddAsync(
                new Season
                {
                    YearStart = (int)season.YearStart,
                    YearEnd = (int)season.YearEnd
                });
    }
}