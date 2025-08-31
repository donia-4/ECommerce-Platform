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
    public class WishlistEntityConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.BuyerId)
                   .IsRequired()
                   .HasMaxLength(450);

            builder.Property(w => w.CreatedAt)
                   .IsRequired();

            // Relationships
            builder.HasOne(w => w.Buyer)
                   .WithOne(b => b.Wishlist)
                   .HasForeignKey<Wishlist>(w => w.BuyerId);

            builder.HasMany(w => w.WishlistItems)
                   .WithOne(wi => wi.Wishlist)
                   .HasForeignKey(wi => wi.WishlistId);
        }
    }

}
