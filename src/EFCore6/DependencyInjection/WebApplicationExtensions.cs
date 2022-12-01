using Microsoft.EntityFrameworkCore;

namespace DependencyInjection
{
    public static class WebApplicationExtensions
    {
        /// <summary>
        /// Utworzenie bazy danych bez migracji
        /// </summary>
        public static WebApplication CreateDatabase<TContext>(this WebApplication app)
            where TContext : DbContext
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<TContext>();

            context.Database.EnsureCreated();

            return app;
        }

        /// <summary>
        /// Utworzenie lub aktualizacja bazy danych na podstawie migracji
        /// </summary>        
        public static WebApplication CreateOrAlterDatabase<TContext>(this WebApplication app)
           where TContext : DbContext
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<TContext>();

            context.Database.Migrate();

            return app;
        }


    }
}
