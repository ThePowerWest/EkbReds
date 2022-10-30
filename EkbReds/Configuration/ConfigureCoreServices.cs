using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Services;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;

namespace Web.Configuration
{
    /// <summary>
    /// Настройка основных сервисов
    /// </summary>
    public static class ConfigureCoreServices
    {
        /// <summary>
        /// Подключение сервисов и репозиториев
        /// </summary>
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IReadRepository<>), typeof(EFRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            services.AddScoped<ISportScoreService, SportScoreService>();
            services.AddScoped<ISeasonService, SeasonService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IScoreService, ScoreService>();
            services.AddScoped<ITournamentService, TournamentService>();

            services.AddScoped<ISeasonRepository, SeasonRepository>();
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITournamentRepository, TournamentRepository>();
            services.AddScoped<IPredictionRepository, PredictionRepository>();

            return services;
        }
    }
}