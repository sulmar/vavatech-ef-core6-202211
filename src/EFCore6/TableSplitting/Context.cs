using Microsoft.EntityFrameworkCore;
using TableSplitting.Models;

namespace TableSplitting
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // public DbSet<Attachment> Attachments { get; set; }

        public DbSet<AttachmentHeader> AttachmentHeaders { get; set; }
        public DbSet<AttachmentContent> AttachmentContents { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
        }


    }
}
