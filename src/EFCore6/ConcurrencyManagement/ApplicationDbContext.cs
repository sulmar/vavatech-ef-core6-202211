using ConcurrencyManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcurrencyManagement
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Customer>()
            //    .Property(p => p.Balance)
            //    .IsConcurrencyToken();

            //modelBuilder.Entity<Customer>()
            //    .Property(p => p.LastName)
            //    .IsConcurrencyToken();

            //modelBuilder.Entity<Customer>()
            //    .Property(p => p.Version)
            //    .IsRowVersion()
            //    .IsConcurrencyToken();

            // Dodanie Version z użyciem Shadow Property
            modelBuilder.Entity<Customer>()
                .Property<byte[]>("Version")
                .IsRowVersion()
                .IsConcurrencyToken();
        }

    }
}
