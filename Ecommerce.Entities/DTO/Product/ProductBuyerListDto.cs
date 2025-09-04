using System;

namespace Ecommerce.Entities.DTO.Product
{
    public class ProductBuyerListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ThumbnailUrl { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public bool InStock { get; set; }
        public double? AverageRating { get; set; }
    }
}