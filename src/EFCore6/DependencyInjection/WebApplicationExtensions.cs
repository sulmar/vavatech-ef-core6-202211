using Microsoft.EntityFrameworkCore;

namespace DependencyInjection
{
    public static class WebApplicationExtensions
    {
        public static WebApplication CreateDatabase<TContext>(this WebApplication app)
            where TContext : DbContext
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<TContext>();

            context.Database.EnsureCreated();

            return app;
        }
    }
}
