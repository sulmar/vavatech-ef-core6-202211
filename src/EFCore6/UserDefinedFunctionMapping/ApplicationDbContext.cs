using Microsoft.EntityFrameworkCore;
using UserDefinedFunctionMapping.Models;

namespace UserDefinedFunctionMapping
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

    }
}
