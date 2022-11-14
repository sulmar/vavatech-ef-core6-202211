using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TableSplitting
{
    internal class ContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            string connectionString = @"Server=(localdb)\mssqllocaldb;Database=AttachmentDb";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .LogTo(Console.Write)
                .UseSqlServer(connectionString).Options;

            return new ApplicationDbContext(options);
        }
    }
}
