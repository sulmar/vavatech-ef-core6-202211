using Microsoft.EntityFrameworkCore;
using TableSplitting.Models;

namespace TableSplitting
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Attachment> Attachments { get; set; }


    }
}
