using ApplicationCore.Interfaces.SportScore;
using ApplicationCore.Models;
using ApplicationCore.Models.SportScore.Teams;
using Newtonsoft.Json;
using System.Text;

namespace ApplicationCore.Services.SportScore
{

    public class MUMatches : IMUMatches
    {
        private const string teamId = "138";
        private const string headerHost = "x-rapidapi-host";
        private const string hederKey = "x-rapidapi-key";
        private const string hostUrl = "sportscore1.p.rapidapi.com";
        private const string rapidKey = "992bc22e39msh4e86352ddcf589fp10ef4djsnb8b62195991a";//"a93e34cda0msh3c73e3045a8444dp11d393jsn51083f1e13c4";//
        //private StringBuilder url = new StringBuilder("https://sportscore1.p.rapidapi.com/{0}/search?");

        public async Task GetNextGame()
        {
            IEnumerable<int> tournamentIds = await GetTournamentsThisSeason();

            HttpResponseMessage response = await GetAsync("https://sportscore1.p.rapidapi.com/teams/138/events?page=1");
            Events events = JsonConvert.DeserializeObject<Events>(await response.Content.ReadAsStringAsync());

            EventData? nextEvent = events.Data.Where(@event => @event.SeasonId == tournamentIds.First())
                                              .OrderBy(@event => @event.StartAt)
                                              .FirstOrDefault(@event => DateTime.Now < @event.StartAt);
        }

        private async Task<IEnumerable<int>> GetTournamentsThisSeason()
        {
            HttpResponseMessage response = await GetAsync("https://sportscore1.p.rapidapi.com/teams/138/seasons");
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

        ///// <summary>
        ///// Загрузить матчи
        ///// </summary>
        //public async Task<Match> LoadAsync()
        //{
        //    string key = Environment.GetEnvironmentVariable("API_KEY");
        //    var client = new HttpClient();
        //    var request = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Get,
        //        RequestUri = new Uri("https://sportscore1.p.rapidapi.com/teams/138/events?page=1"),
        //        Headers =
        //        {
        //            { "X-RapidAPI-Key", key },
        //            { "X-RapidAPI-Host", "sportscore1.p.rapidapi.com" },
        //        }
        //    };
        //    using (var response = await client.SendAsync(request))
        //    {
        //        response.EnsureSuccessStatusCode();
        //        var json = await response.Content.ReadAsStringAsync();
        //        var matches = JsonConvert.DeserializeObject<Match>(json);
        //        matches.Data = matches.Data.Where(m => m.Status == "notstarted" && m.League.NameTranslation.Ru != "Клубные товарищеские матчи");
        //        return matches;
        //    }
        //}
    }
}