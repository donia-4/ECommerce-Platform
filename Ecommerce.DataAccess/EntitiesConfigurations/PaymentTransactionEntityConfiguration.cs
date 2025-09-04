using Ecommerce.Entities.Models;
using Ecommerce.Utilities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.DataAccess.EntitiesConfigurations
{
    public class PaymentTransactionEntityConfiguration : IEntityTypeConfiguration<PaymentTransaction>
    {
        public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
        {
            builder.HasKey(pt => pt.Id);

            // Basic properties
            builder.Property(pt => pt.OrderId)
                   .IsRequired();

            builder.Property(pt => pt.BuyerId)
                   .IsRequired();

            builder.Property(pt => pt.Amount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(pt => pt.Currency)
                   .HasMaxLength(3)
                   .IsRequired();

            // Enum conversions to string
            builder.Property(pt => pt.Status)
                   .HasConversion<string>()
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(pt => pt.Method)
                   .HasConversion<string>()
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(pt => pt.ProviderTxnId)
                   .HasMaxLength(100);

            builder.Property(pt => pt.CreatedAt)
                   .IsRequired();

            // Indexes for better query performance
            builder.HasIndex(pt => pt.OrderId);
            builder.HasIndex(pt => pt.BuyerId);
            builder.HasIndex(pt => pt.Status);
            builder.HasIndex(pt => pt.Method);
            builder.HasIndex(pt => pt.CreatedAt);
            builder.HasIndex(pt => pt.ProviderTxnId)
                   .IsUnique()
                   .HasFilter("[ProviderTxnId] IS NOT NULL");

            builder.ToTable("PaymentTransactions", table =>
                table.HasComment("Stores all payment transactions information"));

            builder.Property(pt => pt.Id)
                   .HasComment("Unique identifier for payment transaction");

            builder.Property(pt => pt.Status)
                   .HasComment("Payment status: Pending, Success, Failed, Refunded");

            builder.HasOne<Order>()
                   .WithMany(o => o.Payments)
                   .HasForeignKey(pt => pt.OrderId) 
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}