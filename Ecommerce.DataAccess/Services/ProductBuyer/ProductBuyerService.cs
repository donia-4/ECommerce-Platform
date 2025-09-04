using Ecommerce.DataAccess.ApplicationContext;
using Ecommerce.Entities.DTO.Product;
using Ecommerce.Entities.Shared;
using Ecommerce.Entities.Shared.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Ecommerce.DataAccess.Services.ProductBuyer
{
    public class ProductBuyerService : IProductBuyerService
    {
        private readonly ApplicationDbContext _context;
        private readonly ResponseHandler _responseHandler;
        private readonly ILogger<ProductBuyerService> _logger;

        public ProductBuyerService(
            ApplicationDbContext context,
            ResponseHandler responseHandler,
            ILogger<ProductBuyerService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Response<PaginatedList<ProductBuyerDetailDto>>> GetProductsAsync(ProductBuyerFilterDto filter)
        {
            try
            {
                var query = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Images)
                    .AsQueryable();

                if (filter.CategoryId.HasValue)
                    query = query.Where(p => p.CategoryId == filter.CategoryId.Value);

                if (filter.MinPrice.HasValue)
                    query = query.Where(p => p.Price >= filter.MinPrice.Value);

                if (filter.MaxPrice.HasValue)
                    query = query.Where(p => p.Price <= filter.MaxPrice.Value);

                query = filter.SortBy switch
                {
                    "priceAsc" => query.OrderBy(p => p.Price),
                    "priceDesc" => query.OrderByDescending(p => p.Price),
                    "nameAsc" => query.OrderBy(p => p.Name),
                    "nameDesc" => query.OrderByDescending(p => p.Name),
                    _ => query.OrderBy(p => p.Name)
                };

                var paginated = await PaginatedList<ProductBuyerDetailDto>.CreateAsync(
                    query.Select(p => new ProductBuyerDetailDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        Stock = p.Stock,
                        Category = p.Category != null ? p.Category.Name : string.Empty,
                        Images = p.Images != null ? p.Images.Select(i => i.Url).ToList() : new List<string>()
                    }),
                    filter.Page,
                    filter.PageSize
                );

                return _responseHandler.Success(paginated, "Products retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products with filters");
                return _responseHandler.InternalServerError<PaginatedList<ProductBuyerDetailDto>>("Failed to fetch products");
            }
        }

        public async Task<Response<ProductBuyerDetailDto>> GetProductByIdAsync(Guid id)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Images)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                    return _responseHandler.NotFound<ProductBuyerDetailDto>("Product not found");

                var dto = new ProductBuyerDetailDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    Category = product.Category != null ? product.Category.Name : string.Empty,
                    Images = product.Images != null ? product.Images.Select(i => i.Url).ToList() : new List<string>()
                };

                return _responseHandler.Success(dto, "Product retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching product");
                return _responseHandler.InternalServerError<ProductBuyerDetailDto>("Failed to fetch product");
            }
        }

        public async Task<Response<PaginatedList<ProductBuyerDetailDto>>> GetProductsByCategoryAsync(Guid categoryId)
        {
            try
            {
                var query = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Images)
                    .Where(p => p.CategoryId == categoryId);

                var paginated = await PaginatedList<ProductBuyerDetailDto>.CreateAsync(
                    query.Select(p => new ProductBuyerDetailDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        Stock = p.Stock,
                        Category = p.Category != null ? p.Category.Name : string.Empty,
                        Images = p.Images != null ? p.Images.Select(i => i.Url).ToList() : new List<string>()
                    }),
                    1,
                    10
                );

                return _responseHandler.Success(paginated, "Products retrieved by category");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products by category");
                return _responseHandler.InternalServerError<PaginatedList<ProductBuyerDetailDto>>("Failed to fetch products by category");
            }
        }

        public async Task<Response<PaginatedList<ProductBuyerDetailDto>>> SearchProductsAsync(string query, int page = 1, int pageSize = 10)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                    return _responseHandler.BadRequest<PaginatedList<ProductBuyerDetailDto>>("Search query cannot be empty");

                var productQuery = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Images)
                    .Where(p =>
                        p.Name.ToLower().Contains(query.ToLower()) ||
                        p.Description.ToLower().Contains(query.ToLower()) ||
                        (p.Category != null && p.Category.Name.ToLower().Contains(query.ToLower()))
                    );

                var paginated = await PaginatedList<ProductBuyerDetailDto>.CreateAsync(
                    productQuery.Select(p => new ProductBuyerDetailDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        Stock = p.Stock,
                        Category = p.Category != null ? p.Category.Name : string.Empty,
                        Images = p.Images != null ? p.Images.Select(i => i.Url).ToList() : new List<string>()
                    }),
                    page,
                    pageSize
                );

                return _responseHandler.Success(paginated, "Products search results");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching products");
                return _responseHandler.InternalServerError<PaginatedList<ProductBuyerDetailDto>>("Failed to search products");
            }
        }
    }
}
