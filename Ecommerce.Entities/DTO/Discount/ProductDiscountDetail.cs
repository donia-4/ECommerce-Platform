using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.DTO.Discount
{
    public class ProductDiscountDetail
    {
        public Guid ProductId { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal PriceAfterDiscount { get; set; }
    }
}
