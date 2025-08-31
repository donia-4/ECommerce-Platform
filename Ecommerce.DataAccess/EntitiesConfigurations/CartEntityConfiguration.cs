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
    public class CartEntityConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.BuyerId)
                   .IsRequired()
                   .HasMaxLength(450);

            builder.Property(c => c.CreatedAt)
                   .IsRequired();

            // Relationships
            builder.HasOne(c => c.Buyer)
                   .WithOne(b => b.Cart)
                   .HasForeignKey<Cart>(c => c.BuyerId);

            builder.HasMany(c => c.CartItems)
                   .WithOne(ci => ci.Cart)
                   .HasForeignKey(ci => ci.CartId);
        }
    }
}
