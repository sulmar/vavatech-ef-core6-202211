using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LazyLoading
{
    internal class ContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        // dotnet add package Microsoft.EntityFrameworkCore.Proxies 
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            string connectionString = @"Server=(localdb)\mssqllocaldb;Database=LazyLoadingDb";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .LogTo(Console.WriteLine)
                // .UseLazyLoadingProxies()
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
