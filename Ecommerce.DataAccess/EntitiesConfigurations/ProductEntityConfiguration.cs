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
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(p => p.Description)
                   .HasMaxLength(1000);

            builder.Property(p => p.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(p => p.Stock)
                   .IsRequired();

            builder.Property(p => p.CategoryId)
                   .IsRequired();

            builder.Property(p => p.CreatedAt)
                   .IsRequired();

            // Relationships
            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId);

            builder.HasMany(p => p.Images)
                   .WithOne(pi => pi.Product)
                   .HasForeignKey(pi => pi.ProductId);

            builder.HasMany(p => p.DiscountLinks)
                   .WithOne(dp => dp.Product)
                   .HasForeignKey(dp => dp.ProductId);
        }
    }
}
