using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.DataAccess.EntitiesConfigurations
{
    public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.OrderId)
                   .IsRequired();

            builder.Property(oi => oi.ProductId)
                   .IsRequired();

            builder.Property(oi => oi.Quantity)
                   .IsRequired();

            builder.Property(oi => oi.UnitPrice)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(oi => oi.Subtotal)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(oi => oi.CreatedAt)
                   .IsRequired();

            // Relationships
            builder.HasOne(oi => oi.Order)
                   .WithMany(o => o.Items)
                   .HasForeignKey(oi => oi.OrderId);

            builder.HasOne(oi => oi.Product)
                   .WithMany()
                   .HasForeignKey(oi => oi.ProductId);
        }
    }
}
