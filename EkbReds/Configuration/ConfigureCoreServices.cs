using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Services;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Web.Interfaces;
using Web.Services;

namespace Web.Configuration
{
    /// <summary>
    /// Настройка основных сервисов
    /// </summary>
    public static class ConfigureCoreServices
    {
        /// <summary>
        /// Подключение сервисов
        /// </summary>
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IReadRepository<>), typeof(EFRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            services.AddScoped<ISportScoreService, SportScoreService>();

            services.AddScoped<ISeasonRepository, SeasonRepository>();
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<IPredictionRepository, PredictionRepository>();
            services.AddScoped<ITournamentRepository, TournamentRepository>();
            services.AddScoped<IScoringService, ScoringService>();

            return services;
        }
    }
}