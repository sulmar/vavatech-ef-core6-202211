using SplitQuery.Models;
using Microsoft.EntityFrameworkCore;

namespace SplitQuery
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Blog> Blogs { get; set; }

    }
}
