﻿using ShadowProperty.Models;
using Microsoft.EntityFrameworkCore;

namespace ShadowProperty
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property<DateTime>("LastUpdated");
        }
    }
}
