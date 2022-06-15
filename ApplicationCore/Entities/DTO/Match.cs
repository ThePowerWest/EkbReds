using Newtonsoft.Json;

namespace ApplicationCore.Entities.DTO
{
    /// <summary>
    /// DTO матча
    /// </summary>
    public class Match
    {
        [JsonProperty("data")]
        public Data[] Data { get; set; }
    }

    /// <summary>
    /// Ветка data
    /// </summary>
    public class Data 
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("start_at")]
        public string Date { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("home_team")]
        public Team[] HomeTeam { get; set; }

        [JsonProperty("away_team")]
        public Team[] AwayTeam { get; set; }

        [JsonProperty("home_score")]
        public Score[] HomeScore { get; set; }

        [JsonProperty("away_score")]
        public Score[] AwayScore { get; set; }

        [JsonProperty("winner_code")]
        public int[] Winner { get; set; }

        [JsonProperty("league")]
        public League[] League { get; set; }

        [JsonProperty("season")]
        public Season[] Season { get; set; }
    }

    /// <summary>
    /// Команда
    /// </summary>
    public class Team 
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("logo")]
        public string? Logo { get; set; }
    }

    /// <summary>
    /// Счет
    /// </summary>
    public class Score 
    {
        [JsonProperty("current")]
        public int Current { get; set; }

        [JsonProperty("period_1")]
        public int Period1 { get; set; }

        [JsonProperty("period_2")]
        public int Period2 { get; set; }
    }

    /// <summary>
    /// Лига
    /// </summary>
    public class League 
    {
        [JsonProperty("name_translations")]
        public NameTranslation[] NameTranslation { get; set; }

        [JsonProperty("logo")]
        public string? Logo { get; set; }
    }

    /// <summary>
    /// Название лиги
    /// </summary>
    public class NameTranslation 
    {
        [JsonProperty("ru")]
        public string Ru { get; set; }
    }

    /// <summary>
    /// Сезон
    /// </summary>
    public class Season 
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
