using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Models.SportScore.Teams;
using ApplicationCore.Specification.Season;
using Newtonsoft.Json;

namespace ApplicationCore.Services
{
    /// <summary>
    /// Сервис для работы с API SportScore
    /// </summary>
    public class SportScoreService : ISportScoreService
    {
        ISeasonRepository SeasonRepository;
        IRepository<Season> SeasonsRepository;
        IReadRepository<SportScoreToken> SportScoreTokenReadRepository;
        Seasons Seasons;
        SeasonLastSpecification spec;

        /// <summary>
        /// ctor
        /// </summary>
        public SportScoreService(ISeasonRepository seasonRepository, IReadRepository<SportScoreToken> sportScoreTokenReadRepository)
        {
            SeasonRepository = seasonRepository;
            SportScoreTokenReadRepository = sportScoreTokenReadRepository;
        }

        private const string teamId = "138";
        private const string headerHost = "x-rapidapi-host";
        private const string hederKey = "x-rapidapi-key";
        private const string hostUrl = "sportscore1.p.rapidapi.com";

        public async Task<List<EventData>> GetNextGames()
        {
            IEnumerable<int> tournamentIds = await GetTournamentsThisSeason();

            HttpResponseMessage response = await GetAsync($"https://{hostUrl}/teams/{teamId}/events?page=1");
            Events events = JsonConvert.DeserializeObject<Events>(await response.Content.ReadAsStringAsync());

            List<EventData> currentEvents = new List<EventData>();
            foreach (EventData @event in events.Data)
            {
                if (tournamentIds.Any(tournamentId => tournamentId == @event.SeasonId))
                {
                    currentEvents.Add(@event);
                }
            }

            return currentEvents.Where(@event => DateTime.Now < @event.StartAt)
                                .OrderBy(@event => @event.StartAt)
                                .ToList();
        }

        /// <summary>
        /// Получить турниры/лиги/чемпионаты в текущем сезоне
        /// </summary>
        /// <returns>Спиоск Id турниров</returns>
        private async Task<IEnumerable<int>> GetTournamentsThisSeason()
        {
            HttpResponseMessage response = await GetAsync($"https://{hostUrl}/teams/{teamId}/seasons");
            Seasons = JsonConvert.DeserializeObject<Seasons>(await response.Content.ReadAsStringAsync());
            string currentDataSeason = Seasons.Data.OrderByDescending(season => season.Id)
                                                   .First().Slug;

            return Seasons.Data.Where(season => season.Slug.Contains(currentDataSeason))
                               .Select(season => season.Id);
        }

        /// <summary>
        /// Отправить Get запрос в SportScore
        /// </summary>
        /// <param name="url">Адрес на который уйдет запрос</param>
        /// <returns>Модель ответа</returns>
        private async Task<HttpResponseMessage> GetAsync(string url)
        {
            IEnumerable<SportScoreToken> sportScoreTokens = await SportScoreTokenReadRepository.ListAsync();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(headerHost, hostUrl);
                client.DefaultRequestHeaders.Add(hederKey, sportScoreTokens.First().Key);

                HttpResponseMessage response = await client.GetAsync(url);
                return response;
            }
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

        /// <summary>
        /// Добавляет сезон в бд
        /// </summary>
        /// <returns></returns>
        private async Task CreateSeason()
        {
            var currentDataSeason = Seasons.Data.OrderByDescending(season => season.Id)
                                                   .First();

            Season season = new Season
            {
                Name = currentDataSeason.Name,
                YearStart = int.Parse(currentDataSeason.YearStart),
                YearEnd = int.Parse(currentDataSeason.YearEnd)
            };
            await SeasonsRepository.AddAsync(season);
        }

        /// <summary>
        /// Добавляет турниры в бд
        /// </summary>
        /// <returns></returns>
        private async Task CreateTournaments()
        {
            var currentDataSeason = Seasons.Data.OrderByDescending(season => season.Id)
                                                   .First();
            List<Tournament> tournaments = new List<Tournament>();
            foreach (var _tournament in Seasons.Data.Where(season => season.Slug.Contains(currentDataSeason.Slug)))
            {
                var tournament = new Tournament
                {
                    Name = _tournament.Name,
                    Season = await SeasonsRepository.FirstOrDefaultAsync(spec)
                };
                tournaments.Add(tournament);
            }
        }

        private async Task AddMatches()
        {
            var events = await GetNextGames();
            var matches = new List<Match>();
            foreach (var _event in events)
            {
                var match = new Match
                {
                    HomeTeamName = _event.HomeTeam.Name,
                    AwayTeamName = _event.AwayTeam.Name,
                    LogoHomeTeam = _event.HomeTeam.Logo,
                    LogoAwayTeam = _event.AwayTeam.Logo,
                    StartDate = _event.StartAt
                };
                matches.Add(match);
            }
        }

        private void GetLastSeason()
        {

        }
    }
}