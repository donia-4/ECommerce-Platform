using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.DTO.Order
{
    public class AdminCreateOrderRequest : CreateOrderRequest
    {
        [Required]
        public string BuyerId { get; set; }

        [Required, MinLength(1)]
        public List<OrderItemRequest> Items { get; set; } = new List<OrderItemRequest>();
    }
}
