using Conversions.Models;
using Microsoft.EntityFrameworkCore;

namespace Conversions
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        
    }
}
