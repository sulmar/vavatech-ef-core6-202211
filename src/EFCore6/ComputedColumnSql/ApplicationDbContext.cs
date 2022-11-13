using ComputedColumnSql.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputedColumnSql
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

    }
}
