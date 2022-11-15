using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GlobalFilters
{
    internal class ContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            string connectionString = @"Server=(localdb)\mssqllocaldb;Database=GlobalFiltersDb";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .LogTo(Console.WriteLine)
                .UseSqlServer(connectionString).Options;

            return new ApplicationDbContext(options);
        }
    }
}
