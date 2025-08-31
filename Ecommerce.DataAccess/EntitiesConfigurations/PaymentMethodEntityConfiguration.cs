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
    public class PaymentMethodEntityConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(pm => pm.Id);

            builder.Property(pm => pm.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(pm => pm.Type)
                   .HasConversion<string>()
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(pm => pm.IsActive)
                   .HasDefaultValue(true);

            // Relationships
            builder.HasMany(pm => pm.Transactions)
                   .WithOne(pt => pt.PaymentMethod)
                   .HasForeignKey(pt => pt.PaymentMethodId);
        }
    }
}
