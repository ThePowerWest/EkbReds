using Hangfire.Dashboard;
using Microsoft.Owin;

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
        public bool Authorize(DashboardContext context)
        {
            // In case you need an OWIN context, use the next line, `OwinContext` class
            // is the part of the `Microsoft.Owin` package.
            var httpContext = context.GetHttpContext();

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return httpContext.User.Identity.IsAuthenticated;
        }
    }
}