using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DataAccess.EntitiesConfigurations
{
    public class DiscountEntityConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Code)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(d => d.Type)
                   .HasConversion<string>()
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(d => d.Value)
                   .HasColumnType("decimal(18,2)");

            builder.Property(d => d.StartDate)
                   .IsRequired();

            builder.Property(d => d.EndDate)
                   .IsRequired();

            builder.Property(d => d.IsActive)
                   .HasDefaultValue(true);

            // Relationships
            builder.HasMany(d => d.Products)
                   .WithOne(dp => dp.Discount)
                   .HasForeignKey(dp => dp.DiscountId);

            builder.HasMany(d => d.Categories)
                   .WithOne(dc => dc.Discount)
                   .HasForeignKey(dc => dc.DiscountId);
        }
    }
}
