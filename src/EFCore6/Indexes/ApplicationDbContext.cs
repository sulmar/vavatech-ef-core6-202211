using Indexes.Models;
using Microsoft.EntityFrameworkCore;

namespace Indexes
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

    }
}
