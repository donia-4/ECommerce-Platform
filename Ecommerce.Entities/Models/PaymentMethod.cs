using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Utilities.Enums;

namespace Ecommerce.Entities.Models
{
    public class PaymentMethod
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public PaymentMethodType Type { get; set; }

        public bool IsActive { get; set; } = true;

        // Relations
        public List<PaymentTransaction> Transactions { get; set; } = new List<PaymentTransaction>();
    }
}
