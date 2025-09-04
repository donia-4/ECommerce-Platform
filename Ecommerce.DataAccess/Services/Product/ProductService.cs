using Ecommerce.DataAccess.ApplicationContext;
using Ecommerce.DataAccess.Services.ImageUploading;
using Ecommerce.Entities.DTO.Product;
using Ecommerce.Entities.Models;
using Ecommerce.Entities.Shared.Bases;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.DataAccess.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageUploadService _imageUploadService;
        private readonly ResponseHandler _responseHandler;
        private readonly ILogger<ProductService> _logger;

        public ProductService(
            ApplicationDbContext context,
            IImageUploadService imageUploadService,
            ResponseHandler responseHandler,
            ILogger<ProductService> logger)
        {
            _context = context;
            _imageUploadService = imageUploadService;
            _responseHandler = responseHandler;
            _logger = logger;
        }

        #region Create Product
        public async Task<Response<ReadProductDto>> CreateProductAsync(CreateProductDto dto)
        {
            try
            {
                if (dto.CategoryId == Guid.Empty)
                    return _responseHandler.BadRequest<ReadProductDto>("CategoryId is required");

                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == dto.CategoryId);
                if (category == null)
                    return _responseHandler.BadRequest<ReadProductDto>("Category does not exist");

                if (dto.UploadImages == null || !dto.UploadImages.Any())
                    return _responseHandler.BadRequest<ReadProductDto>("Image is required");

                if (dto.Stock <= 0)
                    return _responseHandler.BadRequest<ReadProductDto>("Stock must be greater than 0");
                if(dto.Price <= 0)
                    return _responseHandler.BadRequest<ReadProductDto>("Price must be greater than 0");

                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price,
                    Stock = dto.Stock,
                    CategoryId = dto.CategoryId,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                // validate + upload images
                var (imageUrls, errors) = await UploadImages(dto.UploadImages, product.Id, true);
                if (errors.Any())
                    return _responseHandler.BadRequest<ReadProductDto>(string.Join("; ", errors));

                var readDto = new ReadProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    CategoryId = product.CategoryId,
                    CategoryName = category.Name,
                    Images = imageUrls
                };

                return _responseHandler.Success(readDto, "Product created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                return _responseHandler.InternalServerError<ReadProductDto>("Failed to create product");
            }
        }
        #endregion


        #region Update Product
        public async Task<Response<ReadProductDto>> UpdateProductAsync(Guid id, UpdateProductDto dto)
        {
            try
            {
                var product = await _context.Products
                                            .Include(p => p.Images)
                                            .Include(p => p.Category)
                                            .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                    return _responseHandler.NotFound<ReadProductDto>("Product not found");

                // Update category if provided
                if (dto.CategoryId.HasValue && dto.CategoryId.Value != product.CategoryId)
                {
                    var categoryExists = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId.Value);
                    if (!categoryExists)
                        return _responseHandler.BadRequest<ReadProductDto>("Category does not exist");

                    product.CategoryId = dto.CategoryId.Value;
                }

                // Update fields if provided
                if (!string.IsNullOrWhiteSpace(dto.Name))
                    product.Name = dto.Name;

                if (!string.IsNullOrWhiteSpace(dto.Description))
                    product.Description = dto.Description;

       

                if (dto.Stock.HasValue)
                {
                    if (dto.Stock.Value <= 0)
                        return _responseHandler.BadRequest<ReadProductDto>("Stock must be greater than 0");

                    product.Stock = dto.Stock.Value;
                }

                if (dto.Price.HasValue)
                {
                    if (dto.Price <= 0)
                        return _responseHandler.BadRequest<ReadProductDto>("Price must be greater than 0");
                    product.Price = dto.Price.Value;
                }

                // Handle Images (optional in update)
                var imageUrls = product.Images?.Select(i => i.Url).ToList() ?? new List<string>();
                if (dto.UploadImages != null && dto.UploadImages.Any())
                {
                    var (newImages, errors) = await UploadImages(dto.UploadImages, product.Id, false);
                    if (errors.Any())
                        return _responseHandler.BadRequest<ReadProductDto>(string.Join("; ", errors));

                    imageUrls.AddRange(newImages);
                }

                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                var readDto = new ReadProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category?.Name ?? "",
                    Images = imageUrls
                };

                return _responseHandler.Success(readDto, "Product updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product");
                return _responseHandler.InternalServerError<ReadProductDto>("Failed to update product");
            }
        }
        #endregion


        #region Delete Product
        public async Task<Response<bool>> DeleteProductAsync(Guid id)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                    return _responseHandler.NotFound<bool>("Product not found");

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return _responseHandler.Success(true, "Product deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product");
                return _responseHandler.InternalServerError<bool>("Failed to delete product");
            }
        }
        #endregion

        #region Get Products
        public async Task<Response<ReadProductDto>> GetProductByIdAsync(Guid id)
        {
            try
            {
                var product = await _context.Products
                                            .Include(p => p.Images)
                                            .Include(p => p.Category)
                                            .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                    return _responseHandler.NotFound<ReadProductDto>("Product not found");

                var readDto = MapToReadDto(product);

                return _responseHandler.Success(readDto, "Product retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching product");
                return _responseHandler.InternalServerError<ReadProductDto>("Failed to fetch product");
            }
        }

        public async Task<Response<List<ReadProductDto>>> GetAllProductsAsync()
        {
            try
            {
                var products = await _context.Products
                                             .Include(p => p.Images)
                                             .Include(p => p.Category)
                                             .ToListAsync();

                var result = products.Select(MapToReadDto).ToList();

                return _responseHandler.Success(result, "Products retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all products");
                return _responseHandler.InternalServerError<List<ReadProductDto>>("Failed to fetch products");
            }
        }

        public async Task<Response<List<ReadProductDto>>> GetProductsByCategoryAsync(Guid categoryId)
        {
            try
            {
                var products = await _context.Products
                                             .Include(p => p.Images)
                                             .Include(p => p.Category)
                                             .Where(p => p.CategoryId == categoryId)
                                             .ToListAsync();

                var result = products.Select(MapToReadDto).ToList();

                return _responseHandler.Success(result, "Products by category retrieved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products by category");
                return _responseHandler.InternalServerError<List<ReadProductDto>>("Failed to fetch products by category");
            }
        }
        #endregion

        #region Helpers
        private async Task<(List<string> urls, List<string> errors)> UploadImages(IEnumerable<IFormFile>? files, Guid productId, bool isImageRequired)
        {
            var urls = new List<string>();
            var errors = new List<string>();

            if (files == null || !files.Any())
            {
                if (isImageRequired)
                    errors.Add("Image is required");
                return (urls, errors);
            }

            foreach (var file in files)
            {
                if (!IsValidImage(file, out string error))
                {
                    errors.Add(error);
                    continue;
                }

                try
                {
                    var url = await _imageUploadService.UploadAsync(file);
                    _context.ProductImages.Add(new ProductImage
                    {
                        Id = Guid.NewGuid(),
                        ProductId = productId,
                        Url = url,
                        IsPrimary = urls.Count == 0
                    });
                    urls.Add(url);
                }
                catch (Exception ex)
                {
                    errors.Add($"Failed to upload image {file.FileName}: {ex.Message}");
                    _logger.LogWarning(ex, "Failed to upload image {File}", file.FileName);
                }
            }

            if (urls.Any())
                await _context.SaveChangesAsync();
            else if (isImageRequired)
                errors.Add("Image is required");

            return (urls, errors);
        }

        private ReadProductDto MapToReadDto(Product product)
        {
            return new ReadProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? "",
                Images = product.Images?.Select(i => i.Url).ToList() ?? new List<string>()
            };
        }

        private bool IsValidImage(IFormFile file, out string error)
        {
            error = string.Empty;
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".jfif" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                error = $"File {file.FileName} is invalid, must be jpg/jpeg/png/jfif";
                return false;
            }

            if (file.Length > 2 * 1024 * 1024)
            {
                error = $"File {file.FileName} size exceeds 2MB";
                return false;
            }

            return true;
        }
        #endregion
    }
}
