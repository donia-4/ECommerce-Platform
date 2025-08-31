using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.Models
{
    public class DiscountProduct
    {
        [Key]
        public Guid Id { get; set; }

        public Guid DiscountId { get; set; }
        public Guid ProductId { get; set; }

        [ForeignKey(nameof(DiscountId))]
        public Discount Discount { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
    }
}
