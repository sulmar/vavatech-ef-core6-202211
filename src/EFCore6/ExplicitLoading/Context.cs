﻿using ExplicitLoading.Models;
using Microsoft.EntityFrameworkCore;

namespace ExplicitLoading
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

    }
}
