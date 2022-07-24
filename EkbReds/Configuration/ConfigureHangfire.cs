using ApplicationCore.Interfaces.Services;
using Hangfire;
using Hangfire.SqlServer;

namespace Web.Configuration
{
    public static class ConfigureHangfire
    {
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
