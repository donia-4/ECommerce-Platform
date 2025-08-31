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
    public class ProductImageEntityConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasKey(pi => pi.Id);

            builder.Property(pi => pi.ProductId)
                   .IsRequired();

            builder.Property(pi => pi.Url)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(pi => pi.IsPrimary)
                   .IsRequired();

            // Relationships
            builder.HasOne(pi => pi.Product)
                   .WithMany(p => p.Images)
                   .HasForeignKey(pi => pi.ProductId);
        }
    }
}
