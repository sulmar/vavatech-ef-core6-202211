using Inheritance.Models;
using Microsoft.EntityFrameworkCore;

namespace Inheritance
{
    internal class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<RssBlog> RssBlogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Blog>()
                .HasDiscriminator<string>("blog_type")
                .HasValue<Blog>(nameof(Blog))
                .HasValue<RssBlog>(nameof(RssBlogs));
        }

    }
}
