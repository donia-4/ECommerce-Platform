using System;
using System.Collections.Generic;

namespace Ecommerce.Entities.DTO.Product
{
    public class ProductBuyerDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; } = string.Empty;
        public List<string> Images { get; set; } = new List<string>();
        public bool IsOutOfStock => Stock == 0;
    }
}