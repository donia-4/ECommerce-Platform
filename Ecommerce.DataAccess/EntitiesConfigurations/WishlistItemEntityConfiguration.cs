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
    public class WishlistItemEntityConfiguration : IEntityTypeConfiguration<WishlistItem>
    {
        public void Configure(EntityTypeBuilder<WishlistItem> builder)
        {
            builder.HasKey(wi => wi.Id);

            builder.Property(wi => wi.WishlistId)
                   .IsRequired();

            builder.Property(wi => wi.ProductId)
                   .IsRequired();

            builder.Property(wi => wi.CreatedAt)
                   .IsRequired();

            // Relationships
            builder.HasOne(wi => wi.Wishlist)
                   .WithMany(w => w.WishlistItems)
                   .HasForeignKey(wi => wi.WishlistId);

            builder.HasOne(wi => wi.Product)
                   .WithMany()
                   .HasForeignKey(wi => wi.ProductId);
        }
    }
}
