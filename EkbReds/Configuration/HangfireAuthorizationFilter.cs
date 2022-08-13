using Hangfire.Dashboard;

namespace Web.Configuration
{
    /// <summary>
    /// Фильтр для входа в планировщик задач
    /// </summary>
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="context">Контекст планировщика</param>
        /// <returns>Разрешить вход или нет</returns>
        public bool Authorize(DashboardContext context) => context.GetHttpContext().User.Identity.IsAuthenticated;
    }
}