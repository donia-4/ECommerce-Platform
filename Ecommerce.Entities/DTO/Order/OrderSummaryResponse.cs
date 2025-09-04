using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Utilities.Enums;

namespace Ecommerce.Entities.DTO.Order
{
    public class OrderSummaryResponse
    {
        public Guid Id { get; set; }
        public string BuyerName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
    }
}
