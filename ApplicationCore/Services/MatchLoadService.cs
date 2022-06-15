using ApplicationCore.Entities.DTO;
using ApplicationCore.Interfaces;
using Newtonsoft.Json;

namespace ApplicationCore.Services
{
    public class MatchLoadService : IMatchLoadService
    {
        public async Task<List<Match>> LoadAsync()
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
				var matches = JsonConvert.DeserializeObject<List<Match>>(json);
				return matches;
			}
		}
    }
}