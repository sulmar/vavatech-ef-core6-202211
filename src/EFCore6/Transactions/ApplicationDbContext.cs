using Microsoft.EntityFrameworkCore;
using Transactions.Models;

namespace Transactions
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }


    }
}
