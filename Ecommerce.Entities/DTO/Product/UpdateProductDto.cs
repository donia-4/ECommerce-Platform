using Microsoft.AspNetCore.Http;

namespace Ecommerce.Entities.DTO.Product
{
    public class UpdateProductDto
    {
        public string? Name { get; set; } 
        public string? Description { get; set; } 
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public Guid? CategoryId { get; set; }
        public List<IFormFile>? UploadImages { get; set; } 
    }
}
