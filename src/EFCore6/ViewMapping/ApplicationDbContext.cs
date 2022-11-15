using ViewMapping.Models;
using Microsoft.EntityFrameworkCore;

namespace ViewMapping
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .ToTable("ApplicationUsers");
        }
    }


    public class CRMDbContext : DbContext 
    {
        public CRMDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CRMUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<CRMUser>().ToTable("ApplicationUsers", t=>t.ExcludeFromMigrations());

            modelBuilder.Entity<CRMUser>()                
              .ToView("ApplicationUsers");
        }



    }
}
