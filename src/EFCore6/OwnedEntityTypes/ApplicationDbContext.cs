using Microsoft.EntityFrameworkCore;
using OwnedEntityTypes.Models;

namespace OwnedEntityTypes
{

    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
      

    }
}
