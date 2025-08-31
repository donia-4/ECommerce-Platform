using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public Guid CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relations
        public List<ProductImage> Images { get; set; } = new List<ProductImage>();
        public List<DiscountProduct> DiscountLinks { get; set; } = new List<DiscountProduct>();
    }

}
