using ApplicationCore.Interfaces;
using ApplicationCore.Services;
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
            services.AddScoped(typeof(IReadRepository<>), typeof(EFRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISportScoreService, SportScoreService>();

            return services;
        }
    }
}