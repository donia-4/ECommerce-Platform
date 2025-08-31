using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.Models
{
    public class WishlistItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid WishlistId { get; set; }

        [ForeignKey(nameof(WishlistId))]
        public Wishlist Wishlist { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
