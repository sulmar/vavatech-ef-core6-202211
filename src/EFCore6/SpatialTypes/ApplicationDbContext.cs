using SpatialTypes.Models;
using Microsoft.EntityFrameworkCore;

namespace SpatialTypes
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Shop> Shops { get; set; }
    }
}
