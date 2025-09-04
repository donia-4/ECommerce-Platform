using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Entities.Models.Auth;
using Ecommerce.Utilities.Enums;

namespace Ecommerce.Entities.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string BuyerId { get; set; }

        [ForeignKey(nameof(BuyerId))]
        public Buyer Buyer { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }

        public Guid? AppliedDiscountId { get; set; }

        [ForeignKey(nameof(AppliedDiscountId))]
        public Discount? AppliedDiscount { get; set; }

        // Shipping info
        public string ShippingAddress { get; set; }
        public string? ShipPostalCode { get; set; }

        // Relations
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public List<PaymentTransaction>? Payments { get; set; }
    }
}
