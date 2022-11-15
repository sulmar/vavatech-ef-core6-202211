using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ChangeTracking
{
    internal class ContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            string connectionString = @"Server=(localdb)\mssqllocaldb;Database=ChangeTrackingDb";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .LogTo(ConsoleColorLog)
                .UseSqlServer(connectionString).Options;

            return new ApplicationDbContext(options);

            static void ConsoleColorLog(string message)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
    }
}
