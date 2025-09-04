using Ecommerce.Entities.DTO.Product;
using Ecommerce.Entities.Shared.Bases;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Services.Interfaces
{
    public interface IProductService
    {
        Task<Response<ReadProductDto>> CreateProductAsync(CreateProductDto createProductDto);
        Task<Response<ReadProductDto>> UpdateProductAsync( Guid id, UpdateProductDto updateProductDto);
        Task<Response<bool>> DeleteProductAsync(Guid id);
        Task<Response<ReadProductDto>> GetProductByIdAsync(Guid id);
        Task<Response<List<ReadProductDto>>> GetAllProductsAsync();
        Task<Response<List<ReadProductDto>>> GetProductsByCategoryAsync(Guid categoryId);
      
    }
}