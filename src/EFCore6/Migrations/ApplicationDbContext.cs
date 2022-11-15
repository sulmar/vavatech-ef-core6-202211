using Migrations.Models;
using Microsoft.EntityFrameworkCore;

namespace Migrations
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Color> Colors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Color).HasMaxLength(20);

            modelBuilder.Entity<Product>()
                .Property(p=>p.BarCode)
                .IsRequired()
                .HasMaxLength(13)
                .IsUnicode(false);

                modelBuilder.Entity<Color>()
                    .HasData(new List<Color>
                {
                    new Color { Id = 1, Name = "Red"},
                    new Color { Id = 2, Name = "Blue"},
                    new Color { Id = 3, Name = "Green"},
                });
            
        }

    }
}
