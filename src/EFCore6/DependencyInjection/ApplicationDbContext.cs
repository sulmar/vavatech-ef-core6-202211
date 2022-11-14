using DependencyInjection.Models;
using Microsoft.EntityFrameworkCore;

namespace DependencyInjection
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
