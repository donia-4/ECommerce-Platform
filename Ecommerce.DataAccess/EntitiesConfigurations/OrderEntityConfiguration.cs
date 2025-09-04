using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Entities.Models;
using Ecommerce.Utilities.Enums;

namespace Ecommerce.DataAccess.EntitiesConfigurations
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.BuyerId)
                   .IsRequired()
                   .HasMaxLength(450);

            builder.Property(o => o.CreatedAt)
                   .IsRequired();

            builder.Property(o => o.Status)
                   .HasConversion<string>()
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(o => o.Subtotal)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(o => o.DiscountAmount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(o => o.Total)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(o => o.ShippingAddress)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(o => o.ShipPostalCode)
                   .HasMaxLength(20);

            // Relationships
            builder.HasOne(o => o.Buyer)
                   .WithMany(b => b.Orders)
                   .HasForeignKey(o => o.BuyerId);

            builder.HasOne(o => o.AppliedDiscount)
                   .WithMany()
                   .HasForeignKey(o => o.AppliedDiscountId)
                   .IsRequired(false); // Discount is optional

            builder.HasMany(o => o.Items)
                   .WithOne(oi => oi.Order)
                   .HasForeignKey(oi => oi.OrderId);

            builder.HasMany<PaymentTransaction>()
                   .WithOne(p => p.Order);       
        }
    }
}