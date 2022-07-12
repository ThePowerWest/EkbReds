using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.SportScore;
using ApplicationCore.Models;

namespace ApplicationCore.Services
{
    public class SportScoreService : ISportScoreService
    {
        private readonly IMatchLoadService MatchLoadService;
        private readonly IRepository<Entities.Main.Season> SeasonRepository;
        private readonly IRepository<Prediction> PredictionRepository;
        private readonly IRepository<Entities.Main.Match> MatchRepository;
        private readonly IRepository<Entities.Main.League> LeagueRepository;


        /// <summary>
        /// ctor
        /// </summary>
        public SportScoreService
            (IMatchLoadService matchLoadService,
            IRepository<Entities.Main.Season> seasonRepository,
            IRepository<Prediction> predictionRepository,
            IRepository<Entities.Main.Match> matchRepository,
            IRepository<Entities.Main.League> leagueRepository)
        {
            MatchLoadService = matchLoadService;
            SeasonRepository = seasonRepository;
            PredictionRepository = predictionRepository;
            MatchRepository = matchRepository;
            LeagueRepository = leagueRepository;
        }

        public async Task Initialize()
        {
            var allMatches = await MatchLoadService.LoadAsync();
            foreach (var match in allMatches.Data)
            {
                Entities.Main.Match Match = new Entities.Main.Match
                {
                    HomeTeam = match.HomeTeam.Name,
                    LogoHomeTeam = match.HomeTeam.Logo,
                    AwayTeam = match.AwayTeam.Name,
                    LogoAwayTeam = match.AwayTeam.Logo,
                    StartDate = match.Date
                };
                await MatchRepository.AddAsync(Match);
            }
        }
    }
}