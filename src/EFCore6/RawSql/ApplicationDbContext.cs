using RawSql.Models;
using Microsoft.EntityFrameworkCore;

namespace RawSql
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Country> Countries { get; set; }

        public DbSet<TotalAmountCountry> TotalAmountCountries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TotalAmountCountry>().HasNoKey();

            modelBuilder.Entity<Post>()
                .ToView("PostsView");

            // .ToTable("Posts");

            modelBuilder.Entity<TotalAmountCountry>()
                .HasNoKey()
                .ToView("TotalAmountCountryVIEW")
                // .ToFunction("TotalAmountCountryFunction")
                ;
        }


    }
}
