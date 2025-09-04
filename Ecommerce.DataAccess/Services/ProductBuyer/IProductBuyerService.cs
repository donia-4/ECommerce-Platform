using Ecommerce.Entities.DTO.Product;
using Ecommerce.Entities.Shared;
using Ecommerce.Entities.Shared.Bases;
using System;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Services.ProductBuyer
{
    public interface IProductBuyerService
    {

        Task<Response<ProductBuyerDetailDto>> GetProductByIdAsync(Guid id);

        Task<Response<PaginatedList<ProductBuyerDetailDto>>> GetProductsAsync(ProductBuyerFilterDto filter);

        Task<Response<PaginatedList<ProductBuyerDetailDto>>> GetProductsByCategoryAsync(Guid categoryId);

        Task<Response<PaginatedList<ProductBuyerDetailDto>>> SearchProductsAsync(string query, int page = 1, int pageSize = 10);
    }
}
