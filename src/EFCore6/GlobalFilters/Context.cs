using GlobalFilters.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalFilters
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

    }
}
