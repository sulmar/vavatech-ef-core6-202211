using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ViewMapping
{
    internal class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            string connectionString = @"Server=(localdb)\mssqllocaldb;Database=ViewMappingDb";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString).Options;

            return new ApplicationDbContext(options);
        }
    }

    internal class CrmDbContextFactory : IDesignTimeDbContextFactory<CRMDbContext>
    {
        public CRMDbContext CreateDbContext(string[] args)
        {
            string connectionString = @"Server=(localdb)\mssqllocaldb;Database=ViewMappingDb";

            var options = new DbContextOptionsBuilder<CRMDbContext>()
                .UseSqlServer(connectionString).Options;

            return new CRMDbContext(options);
        }
    }
}
