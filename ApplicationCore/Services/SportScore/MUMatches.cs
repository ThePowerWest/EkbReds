using ApplicationCore.Interfaces.SportScore;
using ApplicationCore.Models;
using ApplicationCore.Models.SportScore.Teams;
using Newtonsoft.Json;
using System.Text;

namespace ApplicationCore.Services.SportScore
{
    /// <summary>
    /// 
    /// </summary>
    public class MUMatches : IMUMatches
    {
        private const string teamId = "138";
        private const string headerHost = "x-rapidapi-host";
        private const string hederKey = "x-rapidapi-key";
        private const string hostUrl = "sportscore1.p.rapidapi.com";
        private const string rapidKey = "992bc22e39msh4e86352ddcf589fp10ef4djsnb8b62195991a";//"a93e34cda0msh3c73e3045a8444dp11d393jsn51083f1e13c4";//

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

            return currentEvents.Where(@event => DateTime.Now < @event.StartAt).OrderBy(@event => @event.StartAt).ToList();
        }

        /// <summary>
        /// Получить турниры/лиги/чемпионаты в текущем сезоне
        /// </summary>
        /// <returns>Спиоск Id турниров</returns>
        private async Task<IEnumerable<int>> GetTournamentsThisSeason()
        {
            HttpResponseMessage response = await GetAsync($"https://{hostUrl}/teams/{teamId}/seasons");
            Seasons seasons = JsonConvert.DeserializeObject<Seasons>(await response.Content.ReadAsStringAsync());
            string currentDataSeason = seasons.Data.OrderByDescending(season => season.Id).First().Slug;

            return seasons.Data.Where(season => season.Slug.Contains(currentDataSeason))
                               .Select(season => season.Id);
        }

        private async Task<HttpResponseMessage> Post(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(headerHost, hostUrl);
                client.DefaultRequestHeaders.Add(hederKey, rapidKey);

                HttpResponseMessage response = await client.PostAsync(url, new StringContent(string.Empty, Encoding.UTF8));
                return response;
            }
        }

        private async Task<HttpResponseMessage> GetAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(headerHost, hostUrl);
                client.DefaultRequestHeaders.Add(hederKey, rapidKey);

                HttpResponseMessage response = await client.GetAsync(url);
                return response;
            }
        }
    }
}