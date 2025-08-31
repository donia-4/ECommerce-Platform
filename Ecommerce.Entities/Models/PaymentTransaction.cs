using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Utilities.Enums;

namespace Ecommerce.Entities.Models
{
    public class PaymentTransaction
    {
        [Key]
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }
        public Guid PaymentMethodId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

        [ForeignKey(nameof(PaymentMethodId))]
        public PaymentMethod PaymentMethod { get; set; }

        public decimal Amount { get; set; }
        public PaymentTxnStatus Status { get; set; } = PaymentTxnStatus.Pending;

        public string? ProviderTxnId { get; set; }
        public string? RefundReferenceId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
