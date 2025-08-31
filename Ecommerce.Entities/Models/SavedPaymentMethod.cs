using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Entities.Models.Auth;

namespace Ecommerce.Entities.Models
{
    public class SavedPaymentMethod
    {
        [Key]
        public Guid Id { get; set; }

        public string BuyerId { get; set; }

        [ForeignKey(nameof(BuyerId))]
        public Buyer Buyer { get; set; }

        public Guid PaymentMethodId { get; set; }

        [ForeignKey(nameof(PaymentMethodId))]
        public PaymentMethod PaymentMethod { get; set; }

        public string? Last4 { get; set; }
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;
    }
}
