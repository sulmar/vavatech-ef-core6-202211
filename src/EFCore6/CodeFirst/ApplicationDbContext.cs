using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst
{
    // 1. Zarządzanie połączeniem
    // 2. Konfiguracja modelu i relacji
    // 3. Pobieranie danych
    // 4. Zapis danych
    // 5. Śledzenie zmian
    // 6. Cache'owanie
    // 7. Zarządzanie transakcjami

    // PM> Install-Package Microsoft.EntityFrameworkCore
    // dotnet add package Microsoft.EntityFrameworkCore --version 6.0.0
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetails");

            modelBuilder.Entity<Customer>()
                .Property(p => p.FirstName).HasMaxLength(20);

            modelBuilder.Entity<Customer>()
                .Property(p=>p.ZipCode).IsFixedLength().HasMaxLength(5).IsUnicode(false);
        }


    }
}
