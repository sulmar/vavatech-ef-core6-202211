using ComputedColumnSql.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputedColumnSql
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .Property(p => p.FullName)
                // .HasComputedColumnSql("FirstName + ' ' + LastName")
                .HasComputedColumnSql($"CONCAT({nameof(Customer.FirstName)},' ',{nameof(Customer.LastName)}) PERSISTED");

            // Domyślna wartość na podstawie stałej
            modelBuilder.Entity<Customer>()
                .Property(p => p.Balance)
                .HasDefaultValue(1000);

            // Domyślna wartość na podstawie funkcji SQL
            modelBuilder.Entity<Customer>()
                .Property(p => p.CreatedOn)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Customer>()
                .Property(p => p.ModifiedOn)
                .ValueGeneratedOnUpdate();


        }

    }
}
