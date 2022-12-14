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
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1-do-wielu
            //modelBuilder.Entity<Order>()
            //    .HasOne(p => p.Customer)
            //    .WithMany(p => p.Orders);
            // .HasForeignKey(p => p.PurchaserId);

            //modelBuilder.Entity<Customer>()
            //    .HasMany(o => o.Orders)
            //    .WithOne(p => p.Customer)
            //    .OnDelete(DeleteBehavior.Cascade);

            // 1-do-1
            modelBuilder.Entity<Employee>()
                .HasOne(p => p.EmployeePhoto)
                .WithOne(p => p.Employee)
                .HasForeignKey<EmployeePhoto>("EmployeeId");
            // .HasForeignKey<EmployeePhoto>(p => p.EmployeeRefId);


            modelBuilder.Entity<Post>()
                .HasMany<Tag>(p => p.Tags)
                .WithMany(p => p.Posts)
                .UsingEntity("TagsRelPosts");


        }
    }
}
