using Microsoft.EntityFrameworkCore;
using TemporalTables.Models;

namespace TemporalTables
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

    }
}
