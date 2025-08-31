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
    public class CartItemEntityConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.CartId)
                   .IsRequired();

            builder.Property(ci => ci.ProductId)
                   .IsRequired();

            builder.Property(ci => ci.Quantity)
                   .IsRequired();

            builder.Property(ci => ci.AddedAt)
                   .IsRequired();

            // Relationships
            builder.HasOne(ci => ci.Cart)
                   .WithMany(c => c.CartItems)
                   .HasForeignKey(ci => ci.CartId);

            builder.HasOne(ci => ci.Product)
                   .WithMany()
                   .HasForeignKey(ci => ci.ProductId);
        }
    }
}
