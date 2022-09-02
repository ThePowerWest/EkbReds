using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models;
using ApplicationCore.Models.SportScore.Teams;
using Newtonsoft.Json;
using System.Net;
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
            Season currentSeason = await SeasonRepository.CurrentIncludeTourAsync();
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
                if (foundMatch == null)
                {
                    Tournament tournament = currentTournaments.FirstOrDefault(tournament => tournament.Name == matchSS.Tournament.Name);

                    if (tournament != null)
                    {
                        await MatchCRUDRepository.AddAsync(
                                new Match
                                {
                                    HomeTeamName = matchSS.HomeTeam.Name,
                                    HomeTeamLogo = matchSS.HomeTeam.Logo,
                                    AwayTeamName = matchSS.AwayTeam.Name,
                                    AwayTeamLogo = matchSS.AwayTeam.Logo,
                                    StartDate = matchSS.StartAt,
                                    Tournament = tournament
                                });
                    }
                }
                else
                {
                    if (matchSS.HomeScore != null && matchSS.AwayScore != null)
                    {
                        foundMatch.HomeTeamScore = matchSS.HomeScore.Current;
                        foundMatch.AwayTeamScore = matchSS.AwayScore.Current;

                        await MatchCRUDRepository.UpdateAsync(foundMatch);
                    }
                }
            }
        }

        #region Private region

        /// <summary>
        /// Получить список матчей за этот сезон из SportScore
        /// </summary>
        /// <returns>Список матчей</returns>
        private async Task<IEnumerable<EventData>> GetMatchesThisSeason()
        {
            SSTournament currentSeason = await GetCurrentSeason();
            return await GetMatches(currentSeason.YearEnd);
        }

        /// <summary>
        /// Получить список матчей
        /// </summary>
        /// <param name="yearEnd">Год окончания сезона</param>
        /// <returns>Список матчей</returns>
        private async Task<IEnumerable<EventData>> GetMatches(int? yearEnd)
        {
            HttpResponseMessage response = await GetAsync($"https://{hostUrl}/teams/{teamId}/events?page=1");
            Events events = JsonConvert.DeserializeObject<Events>(await response.Content.ReadAsStringAsync());
            return events.Data.Where(match => match.Tournament != null && match.Tournament.YearEnd == yearEnd);
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
        /// Отправить Get запрос в SportScore
        /// </summary>
        /// <param name="url">Адрес на который уйдет запрос</param>
        /// <returns>Модель ответа</returns>
        private async Task<HttpResponseMessage> GetAsync(string url)
        {
            //System.Net.WebProxy[] proxies = new[] {
            //        null,
            //        new System.Net.WebProxy("89.208.219.121", 8080)
            //    };
            //WebProxyService proxyService = new WebProxyService();
            //proxyService.Proxy = proxies[0];
            IEnumerable<SportScoreToken> sportScoreTokens = await SportScoreTokenReadRepository.ListAsync();
            foreach (SportScoreToken token in sportScoreTokens)
            {
                using (HttpClient client = new HttpClient(/*new HttpClientHandler { UseProxy = true, Proxy = proxyService })*/))
                {
                    Thread.Sleep(5000);
                    client.DefaultRequestHeaders.Add(headerHost, hostUrl);
                    client.DefaultRequestHeaders.Add(hederKey, token.Key);

                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        //using (StreamWriter w = new StreamWriter("test.txt", true, Encoding.GetEncoding(1251)))
                        //{ 
                        //    w.WriteLine($"{token.Key}");
                        //}

                        return response;
                    }
                    //using (StreamWriter w = new StreamWriter("test.txt", true, Encoding.GetEncoding(1251)))
                    //{
                    //    w.WriteLine($"{response.StatusCode}");
                    //}
                }
            }
            return null;
        }

        /// <summary>
        /// Игрок, угадавший точный счет больше остальных
        /// </summary>
        public UserCountModel MostAccuratePlayer(IEnumerable<User> users)
        {
            List<UserCountModel> points = new List<UserCountModel>();
            foreach (User user in users)
            {
                int matchesCount = 0;
                if (user.Predictions != null)
                {
                    foreach (Prediction predict in user.Predictions)
                    {
                        if (predict.Match.HomeTeamScore.HasValue && predict.Match.AwayTeamScore.HasValue)
                        {
                            if (predict.Match.HomeTeamScore.Value == predict.HomeTeamPredict &&
                                predict.Match.AwayTeamScore.Value == predict.AwayTeamPredict)
                            {
                                matchesCount += 1;
                            }
                        }
                    }
                }
                points.Add(new UserCountModel { User = user, Count = matchesCount });
            }
            return points.OrderByDescending(pair => pair.Count).First();
        }
        #endregion  
    }
}