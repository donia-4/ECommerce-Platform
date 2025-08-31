using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Entities.Models.Auth;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Entities.Models;

namespace Ecommerce.DataAccess.EntitiesConfigurations
{
    public class BuyerEntityConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                   .IsRequired()
                   .HasMaxLength(450); // Appropriate length for string FK to User

            builder.Property(b => b.BirthDate)
                   .IsRequired();

            builder.Property(b => b.FullName)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(b => b.CreatedAt)
                   .IsRequired();

            // Relationships
            builder.HasOne(b => b.User)
                   .WithOne()
                   .HasForeignKey<Buyer>(b => b.Id);

            builder.HasOne(b => b.Cart)
                   .WithOne(c => c.Buyer)
                   .HasForeignKey<Cart>(c => c.BuyerId);

            builder.HasOne(b => b.Wishlist)
                   .WithOne(w => w.Buyer)
                   .HasForeignKey<Wishlist>(w => w.BuyerId);

            builder.HasMany(b => b.Orders)
                   .WithOne(o => o.Buyer)
                   .HasForeignKey(o => o.BuyerId);
        }
    }
}
