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
    public class DiscountProductEntityConfiguration : IEntityTypeConfiguration<DiscountProduct>
    {
        public void Configure(EntityTypeBuilder<DiscountProduct> builder)
        {
            builder.HasKey(dp => dp.Id);

            builder.Property(dp => dp.DiscountId)
                   .IsRequired();

            builder.Property(dp => dp.ProductId)
                   .IsRequired();

            // Relationships
            builder.HasOne(dp => dp.Discount)
                   .WithMany(d => d.Products)
                   .HasForeignKey(dp => dp.DiscountId);

            builder.HasOne(dp => dp.Product)
                   .WithMany(p => p.DiscountLinks)
                   .HasForeignKey(dp => dp.ProductId);
        }
    }
}
