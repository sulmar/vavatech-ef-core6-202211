using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeFirst.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .Property(p => p.Name).HasMaxLength(100);

            builder
                .Property(p => p.Barcode).IsFixedLength().HasMaxLength(13).IsUnicode(false);
        }
    }
}
