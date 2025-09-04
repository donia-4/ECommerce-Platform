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
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public PaymentTxnStatus Status { get; set; }
        public PaymentMethodType Method { get; set; }

        public string? ProviderTxnId { get; set; } // Stripe PaymentIntent Id
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }


}
