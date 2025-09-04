using Ecommerce.DataAccess.Services.ProductBuyer;
using Ecommerce.Entities.DTO.Product;
using Ecommerce.Entities.Shared;
using Ecommerce.Entities.Shared.Bases;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductBuyerController : ControllerBase
    {
        private readonly IProductBuyerService _productBuyerService;

        public ProductBuyerController(IProductBuyerService productBuyerService)
        {
            _productBuyerService = productBuyerService ?? throw new ArgumentNullException(nameof(productBuyerService));
        }

        [HttpGet]
        public async Task<ActionResult<Response<PaginatedList<ProductBuyerDetailDto>>>> GetProducts([FromQuery] ProductBuyerFilterDto filter)
        {
            var result = await _productBuyerService.GetProductsAsync(filter);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<ProductBuyerDetailDto>>> GetProductById(Guid id)
        {
            var result = await _productBuyerService.GetProductByIdAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<Response<PaginatedList<ProductBuyerDetailDto>>>> GetProductsByCategory(Guid categoryId)
        {
            var result = await _productBuyerService.GetProductsByCategoryAsync(categoryId);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("search")]
        public async Task<ActionResult<Response<PaginatedList<ProductBuyerDetailDto>>>> SearchProducts([FromQuery] string query, int page = 1, int pageSize = 10)
        {
            var result = await _productBuyerService.SearchProductsAsync(query, page, pageSize);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
