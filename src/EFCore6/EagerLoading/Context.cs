using EagerLoading.Models;
using Microsoft.EntityFrameworkCore;

namespace EagerLoading
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().Navigation(c => c.AccountManager).AutoInclude();
            modelBuilder.Entity<Employee>().Navigation(e => e.Vehicle).AutoInclude();
        }

    }
}
