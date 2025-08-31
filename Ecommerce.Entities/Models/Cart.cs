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
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string BuyerId { get; set; }

        [ForeignKey(nameof(BuyerId))]
        public Buyer Buyer { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
