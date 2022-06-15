using ApplicationCore.Interfaces;
using Infrastructure.Data;

namespace Web.Configuration
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IMatchLoadService, MatchLoadService>();
            //services.AddScoped<IList<Match>, List<Match>>();

            return services;
        }
    }
}