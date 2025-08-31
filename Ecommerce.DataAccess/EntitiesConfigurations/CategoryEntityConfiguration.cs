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
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            // Relationships
            builder.HasMany(c => c.Products)
                   .WithOne(p => p.Category)
                   .HasForeignKey(p => p.CategoryId);
        }
    }
}
