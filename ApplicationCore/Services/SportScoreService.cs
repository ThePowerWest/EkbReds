using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models.SportScore.Teams;
using Newtonsoft.Json;
using SSTournament = ApplicationCore.Models.SportScore.Teams.Tournament;
using Tournament = ApplicationCore.Entities.Main.Tournament;

namespace ApplicationCore.Services
{
    /// <summary>
    /// Реализация сервиса для работы с API SportScore
    /// </summary>
    public class SportScoreService : ISportScoreService
    {
        ISeasonRepository SeasonRepository;
        IRepository<Season> SeasonCRUDRepository;
        IRepository<Tournament> TournamentCRUDRepository;
        IMatchRepository MatchRepository;
        IRepository<Match> MatchCRUDRepository;
        IReadRepository<SportScoreToken> SportScoreTokenReadRepository;
        ITournamentRepository TournamentRepository;

        private const string teamId = "138";
        private const string headerHost = "x-rapidapi-host";
        private const string hederKey = "x-rapidapi-key";
        private const string hostUrl = "sportscore1.p.rapidapi.com";

        /// <summary>
        /// ctor
        /// </summary>
        public SportScoreService(
            ISeasonRepository seasonRepository,
            IReadRepository<SportScoreToken> sportScoreTokenReadRepository,
            IRepository<Season> seasonCRUDRepository,
            IRepository<Tournament> tournamentCRUDRepository,
            IRepository<Match> matchCRUDRepository,
            IMatchRepository matchRepository,
            ITournamentRepository tournamentRepository)
        {
            SeasonRepository = seasonRepository;
            SportScoreTokenReadRepository = sportScoreTokenReadRepository;
            SeasonCRUDRepository = seasonCRUDRepository;
            TournamentCRUDRepository = tournamentCRUDRepository;
            MatchCRUDRepository = matchCRUDRepository;
            MatchRepository = matchRepository;
            TournamentRepository = tournamentRepository;
        }

        /// <inheritdoc />
        public async Task UpdateSeason()
        {
            Season lastSeason = await SeasonRepository.CurrentAsync();
            if (lastSeason == null)
            {
                await CreateSeasonAsync();
            }
            else
            {
                SSTournament currentSeason = await GetCurrentSeason();

                if (lastSeason.YearEnd != currentSeason.YearEnd)
                {
                    await CreateSeasonAsync((int)currentSeason.YearStart, (int)currentSeason.YearEnd);
                }
            }
        }

        /// <inheritdoc />
        public async Task UpdateTournaments()
        {
            Season currentSeason = await SeasonRepository.CurrentAsync();
            IEnumerable<SSTournament> tournaments = await GetTournamentsForThisSeason(currentSeason.YearEnd);

            if (currentSeason.Tournaments == null)
            {
                await TournamentCRUDRepository.AddRangeAsync(tournaments.Select(tournament =>
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
                    SSTournament thisTournament = tournaments.First(tournament => tournament.Name == tournamentName);

                    await TournamentCRUDRepository.AddAsync(
                        new Tournament
                        {
                            Name = thisTournament.Name,
                            Season = currentSeason,
                        });
                }
            }
        }

        /// <inheritdoc />
        public async Task UpdateMatches()
        {
            IEnumerable<Match> currentMatchesDB = MatchRepository.Currents();
            IEnumerable<EventData> currentMatchesSS = await GetMatchesThisSeason();
            IEnumerable<Tournament> currentTournaments = TournamentRepository.Currents();

            foreach (EventData matchSS in currentMatchesSS)
            {
                Match? foundMatch = currentMatchesDB.FirstOrDefault(matchDB => 
                                            matchSS.HomeTeam.Name == matchDB.HomeTeamName &&
                                            matchSS.AwayTeam.Name == matchDB.AwayTeamName &&
                                            matchSS.Tournament.Name == matchDB.Tournament.Name);
                if(foundMatch == null)
                {
                    await MatchCRUDRepository.AddAsync(
                            new Match
                            {
                                HomeTeamName = matchSS.HomeTeam.Name,
                                HomeTeamLogo = matchSS.HomeTeam.Logo,
                                AwayTeamName = matchSS.AwayTeam.Name,
                                AwayTeamLogo = matchSS.AwayTeam.Logo,
                                StartDate = matchSS.StartAt,
                                Tournament = currentTournaments.First(tournament => tournament.Name == matchSS.Tournament.Name)
                            });
                }
                else
                {
                    //foundMatch.

                    await MatchCRUDRepository.UpdateAsync(
                            new Match
                            {
                                HomeTeamName = matchSS.HomeTeam.Name,
                                HomeTeamLogo = matchSS.HomeTeam.Logo,
                                AwayTeamName = matchSS.AwayTeam.Name,
                                AwayTeamLogo = matchSS.AwayTeam.Logo,
                                StartDate = matchSS.StartAt,
                                Tournament = currentTournaments.First(tournament => tournament.Name == matchSS.Tournament.Name)
                            });
                }
            }

            //Season season = await SeasonRepository.CurrentAsync();
            //IEnumerable<Tournament> tournaments = season.Tournaments;

            //IEnumerable<Match> currentMatches = await MatchCRUDRepository.ListAsync();
            //int countMatches = currentMatches.Where(match => DateTime.Now < match.StartDate).Count();
            //if (countMatches == 4) return;

            //IEnumerable<int> tournamentIds = await GetTournamentsThisSeason();

            //HttpResponseMessage response = await GetAsync($"https://{hostUrl}/teams/{teamId}/events?page=1");
            //Events events = JsonConvert.DeserializeObject<Events>(await response.Content.ReadAsStringAsync());

            //List<EventData> currentEvents = new List<EventData>();
            //foreach (EventData @event in events.Data)
            //{
            //    if (tournamentIds.Any(tournamentId => tournamentId == @event.SeasonId))
            //    {
            //        currentEvents.Add(@event);
            //    }
            //}

            //IEnumerable<EventData> newMatches =
            //    currentEvents.Where(@event => DateTime.Now < @event.StartAt)
            //                 .OrderBy(@event => @event.StartAt)
            //                 .Take(4 - countMatches);

            //await MatchCRUDRepository.AddRangeAsync(newMatches.Select(match =>
            //        new Match
            //        {
            //            HomeTeamName = match.HomeTeam.Name,
            //            HomeTeamLogo = match.HomeTeam.Logo,
            //            AwayTeamName = match.AwayTeam.Name,
            //            AwayTeamLogo = match.AwayTeam.Logo,
            //            StartDate = match.StartAt,
            //            Tournament = tournaments.First(tournament => tournament.Name == match.Tournament.Name)
            //        }));
        }

        #region Private region

        private async Task<IEnumerable<EventData>> GetMatchesThisSeason()
        {
            SSTournament currentSeason = await GetCurrentSeason();
            return await GetMatches(currentSeason.YearEnd);
        }

        private async Task<IEnumerable<EventData>> GetMatches(int? yearEnd)
        {
            HttpResponseMessage response = await GetAsync($"https://{hostUrl}/teams/{teamId}/events?page=1");
            Events events = JsonConvert.DeserializeObject<Events>(await response.Content.ReadAsStringAsync());
            return events.Data.Where(match => match.Tournament.YearEnd == yearEnd);
        }

        /// <summary>
        /// Получить текущий сезон из SportScore
        /// </summary>
        /// <returns>Текущий сезон</returns>
        private async Task<SSTournament> GetCurrentSeason()
        {
            HttpResponseMessage response = await GetAsync($"https://{hostUrl}/teams/{teamId}/seasons");
            Seasons seasons = JsonConvert.DeserializeObject<Seasons>(await response.Content.ReadAsStringAsync());
            return seasons.Data
                .Where(season => season.YearEnd != null)
                .OrderByDescending(season => season.YearEnd).First();
        }

        /// <summary>
        /// Получить список всех турниров за этот сезон
        /// </summary>
        /// <param name="yearStart">Год начала сезона</param>
        /// <returns>Список турниров</returns>
        private async Task<IEnumerable<SSTournament>> GetTournamentsForThisSeason(int yearEnd)
        {
            HttpResponseMessage response = await GetAsync($"https://{hostUrl}/teams/{teamId}/seasons");
            Seasons seasons = JsonConvert.DeserializeObject<Seasons>(await response.Content.ReadAsStringAsync());
            return seasons.Data.Where(tournament => tournament.YearEnd == yearEnd);
        }

        /// <summary>
        /// Создать новый сезон
        /// </summary>
        private async Task CreateSeasonAsync()
        {
            SSTournament currentSeason = await GetCurrentSeason();

            await SeasonCRUDRepository.AddAsync(new Season
            {
                YearStart = (int)currentSeason.YearStart,
                YearEnd = (int)currentSeason.YearEnd
            });
        }

        /// <summary>
        /// Создать новый сезон
        /// </summary>
        /// <param name="yearStart">Год начала сезона</param>
        /// <param name="yearEnd">Год окончания сезона</param>
        private async Task CreateSeasonAsync(int yearStart, int yearEnd)
        {
            await SeasonCRUDRepository.AddAsync(new Season
            {
                YearStart = yearStart,
                YearEnd = yearEnd
            });
        }

        /// <summary>
        /// Получить турниры/лиги/чемпионаты в текущем сезоне
        /// </summary>
        /// <returns>Спиcок Id турниров</returns>
        //private async Task<IEnumerable<int>> GetTournamentsThisSeason()
        //{
        //    HttpResponseMessage response = await GetAsync($"https://{hostUrl}/teams/{teamId}/seasons");
        //    Seasons seasons = JsonConvert.DeserializeObject<Seasons>(await response.Content.ReadAsStringAsync());
        //    SSTournament currentSeason = await GetCurrentSeason();
            
        //    HttpResponseMessage response = await GetAsync($"https://{hostUrl}/teams/{teamId}/seasons");
        //    Seasons seasons = JsonConvert.DeserializeObject<Seasons>(await response.Content.ReadAsStringAsync());
        //    string currentDataSeason = seasons.Data.OrderByDescending(season => season.Id)
        //                                           .First().Slug;

        //    return seasons.Data.Where(season => season.Slug.Contains(currentDataSeason))
        //                       .Select(season => season.Id);

        //    return seasons.Data.Where(season => season.Slug.Contains(currentSeason.Slug))
        //                       .Select(season => season.Id);
        //}

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
        #endregion  
    }
}