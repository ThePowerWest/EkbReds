using ApplicationCore.Interfaces.SportScore;
using ApplicationCore.Models;
using Newtonsoft.Json;

namespace ApplicationCore.Services.SportScore
{
    /// <summary>
    /// Сервис для загрузки матчей
    /// </summary>
    public class MatchLoadService : IMatchLoadService
    {
        /// <summary>
        /// Загрузить матчи
        /// </summary>
        public async Task<Match> LoadAsync()
        {
            string key = Environment.GetEnvironmentVariable("API_KEY");
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://sportscore1.p.rapidapi.com/teams/138/events?page=1"),
                Headers =
                {
                    { "X-RapidAPI-Key", key },
                    { "X-RapidAPI-Host", "sportscore1.p.rapidapi.com" },
                }
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var matches = JsonConvert.DeserializeObject<Match>(json);
                matches.Data = matches.Data.Where(m => m.Status == "notstarted" && m.League.NameTranslation.Ru != "Клубные товарищеские матчи");
                return matches;
            }
        }
    }
}