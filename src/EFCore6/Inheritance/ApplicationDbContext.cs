using Inheritance.Models;
using Microsoft.EntityFrameworkCore;

namespace Inheritance
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        //public DbSet<FullTimeEmployee> FullTimeEmployees { get; set; }
        //public DbSet<Contractor> Contractors { get; set; }


        // public DbSet<Item> Items { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TPH (Table-per-hierarchy)
            // Encje potomne znajdują się w jednej tabeli + pole discriminator

            //modelBuilder.Entity<Employee>()
            //    .HasDiscriminator<string>("EmployeeType")
            //    .HasValue<FullTimeEmployee>("FullTime")
            //    .HasValue<Contractor>("Contractor");

            modelBuilder.Entity<Employee>()
                .HasDiscriminator<int>("EmployeeType")
                .HasValue<FullTimeEmployee>(0)
                .HasValue<Contractor>(1);


            // TPT (Table-per-Type)
            // Każda encja potomna znajduje się w osobnej tabeli
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Service>().ToTable("Services");

            // TPC (Table-per-Concrete Class)
            // od EF Core 7.0

        }


    }
}
