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
    public class DiscountCategoryEntityConfiguration : IEntityTypeConfiguration<DiscountCategory>
    {
        public void Configure(EntityTypeBuilder<DiscountCategory> builder)
        {
            builder.HasKey(dc => dc.Id);

            builder.Property(dc => dc.DiscountId)
                   .IsRequired();

            builder.Property(dc => dc.CategoryId)
                   .IsRequired();

            // Relationships
            builder.HasOne(dc => dc.Discount)
                   .WithMany(d => d.Categories)
                   .HasForeignKey(dc => dc.DiscountId);

            builder.HasOne(dc => dc.Category)
                   .WithMany()
                   .HasForeignKey(dc => dc.CategoryId);
        }
    }
}
