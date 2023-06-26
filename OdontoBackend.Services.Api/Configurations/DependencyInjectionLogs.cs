using OdontoBackend.Common.Logs;

namespace OdontoBackend.Services.Api.Configurations
{
    public static class DependencyInjectionLogs
    {
        public static void UseMiddlewareLogs(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            app.UseMiddleware<ExceptionHandler>();
        }
    }
}
