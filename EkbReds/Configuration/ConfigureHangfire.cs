using ApplicationCore.Interfaces.Services;
using Hangfire;
using Hangfire.SqlServer;

namespace Web.Configuration
{
    /// <summary>
    /// Конфигурация шедулера Hangfire
    /// </summary>
    public static class ConfigureHangfire
    {
        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="builder">Билдер веб приложения</param>
        public static void HangfireInitialize(this WebApplicationBuilder builder)
        {
            string connection = Environment.GetEnvironmentVariable("DB_STRING");
            builder.Services.AddHangfire(settings => settings.UseSqlServerStorage(connection));
            JobStorage.Current = new SqlServerStorage(connection);
            builder.Services.AddHangfireServer();

            #region Задачи
            RecurringJob.AddOrUpdate<ISportScoreService>("Обновить сезон", job => job.UpdateSeason(), "0 */23 * * *");
            RecurringJob.AddOrUpdate<ISportScoreService>("Обновить турниры", job => job.UpdateTournaments(), "0 */23 * * *");
            RecurringJob.AddOrUpdate<ISportScoreService>("Обновить матчи", job => job.UpdateMatches(), "0 */23 * * *");
            #endregion
        }
    }
}
