using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models.SportScore.Teams;
using Newtonsoft.Json;
using System.Net;
using SportScoreTournament = ApplicationCore.Models.SportScore.Teams.Tournament;

namespace ApplicationCore.Services
{
    /// <summary>
    /// Реализация сервиса для работы с API SportScore
    /// </summary>
    public class SportScoreService : ISportScoreService
    {
        private readonly IReadRepository<SportScoreToken> _sportScoreTokenReadRepository;
        private readonly IReadRepository<Proxy> _proxyRepository;

        private const string teamId = "138";
        private const string headerHost = "x-rapidapi-host";
        private const string hederKey = "x-rapidapi-key";
        private const string hostUrl = "sportscore1.p.rapidapi.com";

        /// <summary>
        /// ctor
        /// </summary>
        public SportScoreService(
            IReadRepository<SportScoreToken> sportScoreTokenReadRepository,
            IReadRepository<Proxy> proxyRepository)
        {
            _sportScoreTokenReadRepository = sportScoreTokenReadRepository;
            _proxyRepository = proxyRepository;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SportScoreTournament>> GetTournamentsCurrentSeasonAsync(int yearEnd)
        {
            string response = await GetAsync($"https://{hostUrl}/teams/{teamId}/seasons");
            Seasons seasons = JsonConvert.DeserializeObject<Seasons>(response);
            return seasons.Data.Where(tournament => tournament.YearEnd == yearEnd);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<EventData>> AllMatchesAsync()
        {
            string response = await GetAsync($"https://{hostUrl}/teams/{teamId}/events?page=1");
            Events events = JsonConvert.DeserializeObject<Events>(response);
            return events.Data.Where(match => match.Tournament != null);
        }

        /// <inheritdoc />
        public async Task<SportScoreTournament> CurrentSeasonAsync()
        {
            string response = await GetAsync($"https://{hostUrl}/teams/{teamId}/seasons");
            Seasons seasons = JsonConvert.DeserializeObject<Seasons>(response);
            return seasons.Data
                .Where(season => season.YearEnd != null)
                .OrderByDescending(season => season.YearEnd)
                .First();
        }

        #region Private region 
        /// <summary>
        /// Отправить Get запрос в SportScore
        /// </summary>
        /// <param name="url">Адрес на который уйдет запрос</param>
        /// <returns>Модель ответа</returns>
        private async Task<string> GetAsync(string url)
        {
            IEnumerable<SportScoreToken> sportScoreTokens = await _sportScoreTokenReadRepository.ListAsync();
            IEnumerable<Proxy> proxy = await _proxyRepository.ListAsync();
            foreach (SportScoreToken token in sportScoreTokens)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                WebProxy myProxy = new();

                myProxy.Address = new Uri($"http://{proxy.First().IP}");
                myProxy.Credentials = new NetworkCredential(proxy.First().Login, proxy.First().Password);

                request.Proxy = myProxy;
                request.Headers.Add(headerHost, hostUrl);
                request.Headers.Add(hederKey, token.Key);

                WebResponse response = await request.GetResponseAsync();

                if ((response as HttpWebResponse).StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string data = reader.ReadToEnd();

                        return data;
                    }
                }
            }
            return null;
        }
        #endregion
    }
}