using ConcurrencyManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcurrencyManagement
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

    }
}
