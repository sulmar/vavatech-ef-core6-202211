using Relationships.Models;
using Microsoft.EntityFrameworkCore;

namespace Relationships
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1-do-wielu
            //modelBuilder.Entity<Order>()
            //    .HasOne(p => p.Customer)
            //    .WithMany(p => p.Orders);
            // .HasForeignKey(p => p.PurchaserId);

            // 1-do-1
            modelBuilder.Entity<Employee>()
                .HasOne(p => p.EmployeePhoto)
                .WithOne(p => p.Employee)
                .HasForeignKey<EmployeePhoto>("EmployeeId");
            // .HasForeignKey<EmployeePhoto>(p => p.EmployeeRefId);


        }
    }
}
