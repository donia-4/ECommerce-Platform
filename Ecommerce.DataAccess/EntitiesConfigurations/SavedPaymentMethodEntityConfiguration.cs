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
    public class SavedPaymentMethodEntityConfiguration : IEntityTypeConfiguration<SavedPaymentMethod>
    {
        public void Configure(EntityTypeBuilder<SavedPaymentMethod> builder)
        {
            builder.HasKey(spm => spm.Id);

            builder.Property(spm => spm.BuyerId)
                   .IsRequired()
                   .HasMaxLength(450);

            builder.Property(spm => spm.PaymentMethodId)
                   .IsRequired();

            builder.Property(spm => spm.Last4)
                   .HasMaxLength(4);

            builder.Property(spm => spm.SavedAt)
                   .IsRequired();

            // Relationships
            builder.HasOne(spm => spm.Buyer)
                   .WithMany()
                   .HasForeignKey(spm => spm.BuyerId);

            builder.HasOne(spm => spm.PaymentMethod)
                   .WithMany()
                   .HasForeignKey(spm => spm.PaymentMethodId);
        }
    }
}
