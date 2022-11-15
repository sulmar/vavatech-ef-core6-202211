using LazyLoading.Models;
using Microsoft.EntityFrameworkCore;

namespace LazyLoading
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
      //  public DbSet<Blog> Blogs { get; set; }

    }
}
