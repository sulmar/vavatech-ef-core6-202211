using Migrations.Models;
using Microsoft.EntityFrameworkCore;

namespace Migrations
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

    }
}
