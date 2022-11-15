using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EagerLoading
{
    internal class ContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            string connectionString = @"Server=(localdb)\mssqllocaldb;Database=EagerLoadingDb";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .LogTo(Console.WriteLine)
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
