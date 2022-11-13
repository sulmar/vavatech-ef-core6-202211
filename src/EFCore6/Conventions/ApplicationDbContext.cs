using Conventions.Models;
using Microsoft.EntityFrameworkCore;

namespace Conventions
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        
    }
}
