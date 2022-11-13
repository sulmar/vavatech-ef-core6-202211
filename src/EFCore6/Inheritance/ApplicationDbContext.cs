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
        public DbSet<Item> Items { get; set; }


    }
}
