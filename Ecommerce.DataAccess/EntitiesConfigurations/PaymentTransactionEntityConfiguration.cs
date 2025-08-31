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
    public class PaymentTransactionEntityConfiguration : IEntityTypeConfiguration<PaymentTransaction>
    {
        public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
        {
            builder.HasKey(pt => pt.Id);

            builder.Property(pt => pt.OrderId)
                   .IsRequired();

            builder.Property(pt => pt.PaymentMethodId)
                   .IsRequired();

            builder.Property(pt => pt.Amount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(pt => pt.Status)
                   .HasConversion<string>()
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(pt => pt.ProviderTxnId)
                   .HasMaxLength(100);

            builder.Property(pt => pt.RefundReferenceId)
                   .HasMaxLength(100);

            builder.Property(pt => pt.CreatedAt)
                   .IsRequired();

            // Relationships
            builder.HasOne(pt => pt.Order)
                   .WithOne(o => o.Payment)
                   .HasForeignKey<PaymentTransaction>(pt => pt.OrderId);

            builder.HasOne(pt => pt.PaymentMethod)
                   .WithMany(pm => pm.Transactions)
                   .HasForeignKey(pt => pt.PaymentMethodId);
        }
    }
}
