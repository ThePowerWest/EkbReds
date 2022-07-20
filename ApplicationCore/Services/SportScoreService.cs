using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Specification.Season;

namespace ApplicationCore.Services
{
    /// <summary>
    /// Сервис для работы с API SportScore
    /// </summary>
    public class SportScoreService : ISportScoreService
    {
        ISeasonRepository SeasonRepository;

        /// <summary>
        /// ctor
        /// </summary>
        public SportScoreService(ISeasonRepository seasonRepository) 
        {
            SeasonRepository = seasonRepository;
        }

        /// <summary>
        /// Обновление матчей
        /// </summary>
        public void UpdateMatches()
        {
            CheckSeason();
        }

        /// <summary>
        /// Проверим, начался ли новый сезон
        /// </summary>
        private async Task CheckSeason()
        {
            Season lastSeason = await SeasonRepository.LastAsync();
            if (lastSeason == null)
            {
                CreateSeason();
            }
            else
            {

            }
        }

        private void CreateSeason()
        {

        }

        private void GetLastSeason()
        {

        }
    }
}