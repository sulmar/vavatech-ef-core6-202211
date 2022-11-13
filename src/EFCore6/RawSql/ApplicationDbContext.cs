using RawSql.Models;
using Microsoft.EntityFrameworkCore;

namespace RawSql
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

       

    }
}
