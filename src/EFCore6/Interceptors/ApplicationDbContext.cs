using Microsoft.EntityFrameworkCore;
using Interceptors.Models;

namespace Interceptors
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

    }
}
