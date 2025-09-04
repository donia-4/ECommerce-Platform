using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Entities.DTO.Order
{
    public class CreateOrderRequest
    {
        [Required]
        public string ShippingAddress { get; set; }
        public string? ShipPostalCode { get; set; }
    }
}
