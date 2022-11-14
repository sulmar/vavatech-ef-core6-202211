using Conversions.Converters;
using Conversions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Conversions.Configurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // builder.Ignore(p => p.DateOfBirth);

            builder.Ignore(p => p.WakeupHour);

            // builder.Ignore(p => p.Location);

            //builder.Property(p => p.Profile)
            //    .HasConversion(
            //        profile => JsonSerializer.Serialize(profile, (JsonSerializerOptions) null),
            //        value => JsonSerializer.Deserialize<Profile>(value, (JsonSerializerOptions) null)
            //    );

            builder.Property(p => p.Profile)
                .HasConversion<JsonConverter<Profile>>();

            builder.Property(p => p.DateOfBirth)
                .HasConversion<DateOnlyConverter>();


            builder.Property(p => p.Location)
                .HasConversion<GeoHashConverter>();


            builder.Property(p => p.CanSend)
                .HasConversion(new BoolToStringConverter("No", "Yes"));
        }
    }
}
