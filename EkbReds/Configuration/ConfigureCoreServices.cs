using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.SportScore;
using ApplicationCore.Services;
using ApplicationCore.Services.SportScore;
using Infrastructure.Data;

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
            services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMUMatches, MUMatches>();
            services.AddScoped<ISportScoreService, SportScoreService>();

            return services;
        }
    }
}