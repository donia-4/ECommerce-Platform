using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.DTO.Discount
{
    public class ApplyDiscountResponse
    {
        public bool IsApplied { get; set; }
        public string Message { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NewTotal { get; set; }
        public List<ProductDiscountDetail> Products { get; set; } = new();
    }
}
