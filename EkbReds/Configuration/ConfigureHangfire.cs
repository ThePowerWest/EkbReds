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
            RecurringJob.AddOrUpdate<ISeasonService>("Создать новый сезон", job => job.CreateAsync(), "0 0 1 * *");
            RecurringJob.AddOrUpdate<ITournamentService>("Создать новый турнир", job => job.CreateAsync(), "0 0 */3 * *");
            RecurringJob.AddOrUpdate<IMatchService>("Создать или обновить матчи", job => job.CreateOrUpdateAsync(), "0 0 * * *");
            #endregion
        }
    }
}