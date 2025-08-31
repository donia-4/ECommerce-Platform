using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.Models
{
    public class ProductImage
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        [Required]
        public string Url { get; set; }

        public bool IsPrimary { get; set; }
    }
}
