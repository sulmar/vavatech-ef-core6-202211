using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ExplicitLoading
{
    internal class ContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            string connectionString = @"Server=(localdb)\mssqllocaldb;Database=ExplicitLoadingDb";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .LogTo(Console.WriteLine)
                .UseSqlServer(connectionString).Options;

            return new ApplicationDbContext(options);
        }
    }
}
