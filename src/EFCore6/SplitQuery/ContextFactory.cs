using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SplitQuery
{
    internal class ContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            string connectionString = @"Server=(localdb)\mssqllocaldb;Database=SplitQueryDb";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .LogTo(Console.WriteLine)
                .UseSqlServer(connectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
