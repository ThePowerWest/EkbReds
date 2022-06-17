using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    /// <summary>
    /// Зависимости
    /// </summary>
    public static class Dependencies
    {
        /// <summary>
        /// Подключение SQL Server
        /// </summary>
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<MainContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("DB_STRING")));
        }
    }
}