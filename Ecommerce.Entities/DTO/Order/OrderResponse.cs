using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Utilities.Enums;

namespace Ecommerce.Entities.DTO.Order
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public string BuyerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }
        public string ShippingAddress { get; set; }
        public string? ShipPostalCode { get; set; }
        public List<OrderItemResponse> Items { get; set; } = new List<OrderItemResponse>();
    }
}
