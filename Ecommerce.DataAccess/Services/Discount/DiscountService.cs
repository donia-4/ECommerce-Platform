using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.DataAccess.ApplicationContext;
using Ecommerce.Entities.DTO.Discount;
using Ecommerce.Entities.Shared.Bases;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Entities.Shared;
using Ecommerce.Utilities.Enums;

namespace Ecommerce.DataAccess.Services.Discount
{

    public class DiscountService : IDiscountService
    {
        private readonly ApplicationDbContext _db;
        private readonly ResponseHandler _response;
        private readonly ILogger<DiscountService> _logger;
        public DiscountService(ApplicationDbContext db, ResponseHandler response, ILogger<DiscountService> logger)
        {
            _db = db; _response = response; _logger = logger;
        }

        public async Task<Response<Guid>> CreateAsync(CreateDiscountRequest request)
        {
            try
            {
                var codeExists = await _db.Discounts.AsNoTracking().AnyAsync(d => d.Code == request.Code);
                if (codeExists)
                    return _response.BadRequest<Guid>("A discount with the same code already exists.");

                if (!Enum.TryParse<DiscountType>(request.Type, true, out var parsedType))
                    return _response.BadRequest<Guid>("Invalid discount type.");

                var now = DateTime.UtcNow;

                DiscountStatus status;
                if (now >= request.StartDate && now <= request.EndDate)
                    status = DiscountStatus.Active;
                else if (now > request.EndDate)
                    status = DiscountStatus.Expired;
                else
                    status = DiscountStatus.NoDiscount;

                var entity = new Ecommerce.Entities.Models.Discount
                {
                    Id = Guid.NewGuid(),
                    Code = request.Code,
                    Type = parsedType,
                    Value = request.Value,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    IsActive = now >= request.StartDate && now <= request.EndDate, // active if in range
                    Status = status
                };

                if (request.ProductIds?.Count > 0)
                {
                    var links = request.ProductIds.Select(pid => new Ecommerce.Entities.Models.DiscountProduct
                    {
                        Id = Guid.NewGuid(),
                        DiscountId = entity.Id,
                        ProductId = pid
                    });
                    await _db.DiscountProducts.AddRangeAsync(links);
                }

                if (request.CategoryIds?.Count > 0)
                {
                    var links = request.CategoryIds.Select(cid => new Ecommerce.Entities.Models.DiscountCategory
                    {
                        Id = Guid.NewGuid(),
                        DiscountId = entity.Id,
                        CategoryId = cid
                    });
                    await _db.DiscountCategories.AddRangeAsync(links);
                }

                await _db.Discounts.AddAsync(entity);
                await _db.SaveChangesAsync();

                _logger.LogInformation("Discount {Code} created with id {Id}", entity.Code, entity.Id);
                return _response.Created(entity.Id, "Discount created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating discount");
                return _response.ServerError<Guid>("Failed to create discount.");
            }
        }

        public async Task<Response<Guid>> UpdateAsync(Guid id, UpdateDiscountRequest request)
        {
            try
            {
                var entity = await _db.Discounts.Include(d => d.Products).Include(d => d.Categories)
                    .FirstOrDefaultAsync(d => d.Id == id);
                if (entity == null)
                    return _response.NotFound<Guid>("Discount not found.");

                // unique code (exclude current)
                var codeExists = await _db.Discounts.AsNoTracking()
                    .AnyAsync(d => d.Code == request.Code && d.Id != id);
                if (codeExists)
                    return _response.BadRequest<Guid>("Another discount with the same code already exists.");

                if (!Enum.TryParse<DiscountType>(request.Type, true, out var parsedType))
                    return _response.BadRequest<Guid>("Invalid discount type.");

                    entity.Code = request.Code;
                entity.Type = parsedType;
                entity.Value = request.Value;
                entity.StartDate = request.StartDate;
                entity.EndDate = request.EndDate;
                if (request.IsActive.HasValue)
                    entity.IsActive = request.IsActive.Value;

                // Replace relations (simple approach: remove then add)
                var oldProdLinks = await _db.DiscountProducts.Where(x => x.DiscountId == id).ToListAsync();
                var oldCatLinks = await _db.DiscountCategories.Where(x => x.DiscountId == id).ToListAsync();
                _db.DiscountProducts.RemoveRange(oldProdLinks);
                _db.DiscountCategories.RemoveRange(oldCatLinks);

                if (request.ProductIds?.Count > 0)
                {
                    var newProdLinks = request.ProductIds.Select(pid => new Ecommerce.Entities.Models.DiscountProduct
                    {
                        Id = Guid.NewGuid(),
                        DiscountId = id,
                        ProductId = pid
                    });
                    await _db.DiscountProducts.AddRangeAsync(newProdLinks);
                }

                if (request.CategoryIds?.Count > 0)
                {
                    var newCatLinks = request.CategoryIds.Select(cid => new Ecommerce.Entities.Models.DiscountCategory
                    {
                        Id = Guid.NewGuid(),
                        DiscountId = id,
                        CategoryId = cid
                    });
                    await _db.DiscountCategories.AddRangeAsync(newCatLinks);
                }

                await _db.SaveChangesAsync();
                _logger.LogInformation("Discount {Id} updated", id);
                return _response.Success(id, "Discount updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating discount {Id}", id);
                return _response.ServerError<Guid>("Failed to update discount.");
            }
        }

        public async Task<Response<GetDiscountResponse>> GetByIdAsync(Guid id)
        {
            try
            {
                var dto = await _db.Discounts.AsNoTracking()
                .Where(d => d.Id == id)
                .Select(d => new GetDiscountResponse
                {
                    Id = d.Id,
                    Code = d.Code,
                    Type = d.Type.ToString(),
                    Value = d.Value,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate,
                    IsActive = d.IsActive,
                    IsCurrentlyActive = d.IsActive && DateTime.UtcNow >= d.StartDate && DateTime.UtcNow <= d.EndDate,
                    GuidProductIds = d.Products.Select(p => p.ProductId).ToList(),
                    GuidCategoryIds = d.Categories.Select(c => c.CategoryId).ToList(),
                })
                .FirstOrDefaultAsync();

                if (dto == null)
                    return _response.NotFound<GetDiscountResponse>("Discount not found.");

                return _response.Success(dto, "Discount retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error finding the discount");
                return _response.ServerError<GetDiscountResponse>("Failed to find discount.");
            }

        }

        public async Task<Response<List<GetDiscountResponse>>> GetAllAsync(bool? onlyActive = null)
        {
            try
            {
                var q = _db.Discounts.AsNoTracking().AsQueryable();
                var now = DateTime.UtcNow;
                if (onlyActive == true)
                {
                    q = q.Where(d => d.IsActive && d.StartDate <= now && d.EndDate >= now);
                }
                var list = await q
                    .Select(d => new GetDiscountResponse
                    {
                        Id = d.Id,
                        Code = d.Code,
                        Type = d.Type.ToString(),
                        Value = d.Value,
                        StartDate = d.StartDate,
                        EndDate = d.EndDate,
                        IsActive = d.IsActive,
                        IsCurrentlyActive = d.IsActive && now >= d.StartDate && now <= d.EndDate,
                        GuidProductIds = d.Products.Select(p => p.ProductId).ToList(),
                        GuidCategoryIds = d.Categories.Select(c => c.CategoryId).ToList(),
                    })
                    .ToListAsync();

                if (list.Count == 0)
                    return _response.NotFound<List<GetDiscountResponse>>("No discounts found.");

                return _response.Success(list, "Discounts retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving discounts");
                return _response.ServerError<List<GetDiscountResponse>>("Failed to retrieve discounts.");
            }
        }

        public async Task<Response<bool>> DeactivateAsync(Guid id)
        {
            try
            {
                var entity = await _db.Discounts.FirstOrDefaultAsync(d => d.Id == id);
                if (entity == null)
                    return _response.NotFound<bool>("Discount not found.");

                entity.IsActive = false;
                await _db.SaveChangesAsync();
                return _response.Success(true, "Discount deactivated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deactivating discount");
                return _response.ServerError<bool>("Failed to deactivate the discount.");
            }
        }
        public async Task<Response<PaginatedList<GetDiscountResponse>>> GetPaginatedAsync(GetDiscountsQuery query)
        {
            try
            {
                var q = _db.Discounts.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(query.Status) && Enum.TryParse<DiscountStatus>(query.Status, true, out var parsedStatus))
                {
                    var now = DateTime.UtcNow;
                    if (parsedStatus == DiscountStatus.Active)
                    {
                        q = q.Where(d => d.IsActive && d.StartDate <= now && d.EndDate >= now);
                    }
                    else if (parsedStatus == DiscountStatus.Expired)
                    {
                        q = q.Where(d => d.EndDate < now || !d.IsActive);
                    }
                }

                if (!string.IsNullOrEmpty(query.Code))
                {
                    q = q.Where(d => d.Code.Contains(query.Code));
                }

                var totalCount = await q.CountAsync();

                var discounts = await q
                    .OrderByDescending(d => d.StartDate)
                    .Skip((query.PageNumber - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .Select(d => new GetDiscountResponse
                    {
                        Id = d.Id,
                        Code = d.Code,
                        Type = d.Type.ToString(),
                        Value = d.Value,
                        StartDate = d.StartDate,
                        EndDate = d.EndDate,
                        IsActive = d.IsActive,
                        IsCurrentlyActive = d.IsActive && DateTime.UtcNow >= d.StartDate && DateTime.UtcNow <= d.EndDate
                    })
                    .ToListAsync();

                if (!discounts.Any())
                    return _response.NotFound<PaginatedList<GetDiscountResponse>>("No discounts found.");

                var result = new PaginatedList<GetDiscountResponse>(discounts, totalCount, query.PageNumber, query.PageSize);
                return _response.Success(result, "Discounts retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting paginated or filtered discounts");
                return _response.ServerError<PaginatedList<GetDiscountResponse>>("Failed to get paginated or filtered discounts.");
            }

        }
        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await _db.Discounts.FirstOrDefaultAsync(d => d.Id == id);
                if (entity == null)
                    return _response.NotFound<bool>("Discount not found.");

                _db.Discounts.Remove(entity);
                await _db.SaveChangesAsync();
                return _response.Success(true, "Discount deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting the discount");
                return _response.ServerError<bool>("Failed to delete discount.");
            }
        }

        public async Task<Response<List<GetActiveDiscountsResponse>>> GetActiveDiscountsAsync(GetActiveDiscountsRequest request)
        {
            try
            {
                var now = DateTime.UtcNow;

                var query = _db.Discounts
                    .AsNoTracking()
                    .Where(d => d.IsActive && d.StartDate <= now && d.EndDate >= now);

                if (request.ProductIds?.Any() == true)
                {
                    query = query.Where(d => d.Products.Any(p => request.ProductIds.Contains(p.ProductId)));
                }

                if (request.CategoryIds?.Any() == true)
                {
                    query = query.Where(d => d.Categories.Any(c => request.CategoryIds.Contains(c.CategoryId)));
                }

                var list = await query
                    .Select(d => new GetActiveDiscountsResponse
                    {
                        Id = d.Id,
                        Code = d.Code,
                        Type = d.Type.ToString(),
                        Value = d.Value,
                        StartDate = d.StartDate,
                        EndDate = d.EndDate,
                        IsActive = d.IsActive
                    })
                    .ToListAsync();

                if (!list.Any())
                    return _response.NotFound<List<GetActiveDiscountsResponse>>("No active discounts found.");

                return _response.Success(list, "Active discounts retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching active discounts");
                return _response.ServerError<List<GetActiveDiscountsResponse>>("Failed to fetch discounts.");
            }
        }

        public async Task<Response<ApplyDiscountResponse>> ApplyDiscountAsync(ApplyDiscountRequest request)
        {
            try
            {
                var discount = await _db.Discounts
                    .Include(d => d.Products)
                    .FirstOrDefaultAsync(d => d.Code == request.Code && d.IsActive);

                if (discount == null)
                    return _response.BadRequest<ApplyDiscountResponse>("Invalid or expired discount code.");

                var now = DateTime.UtcNow;
                if (now < discount.StartDate || now > discount.EndDate)
                    return _response.BadRequest<ApplyDiscountResponse>("Discount code is expired.");

                var products = await _db.Products
                    .Where(p => request.ProductIds.Contains(p.Id))
                    .Select(p => new { p.Id, p.Price })
                    .ToListAsync();

                if (!products.Any())
                    return _response.BadRequest<ApplyDiscountResponse>("No valid products found.");

                bool applicable = discount.Products.Any(p => request.ProductIds.Contains(p.ProductId));
                if (!applicable)
                    return _response.BadRequest<ApplyDiscountResponse>("Discount not applicable to selected products.");

                decimal totalOriginal = products.Sum(p => p.Price);
                decimal discountAmount = 0;
                var productDetails = new List<ProductDiscountDetail>();

                foreach (var product in products)
                {
                    decimal priceAfterDiscount = product.Price;

                    if (discount.Type == DiscountType.Percentage)
                    {
                        priceAfterDiscount -= (priceAfterDiscount * (discount.Value / 100));
                    }
                    else if (discount.Type == DiscountType.FixedAmount)
                    {
                        priceAfterDiscount -= discount.Value;
                        if (priceAfterDiscount < 0) priceAfterDiscount = 0; 
                    }

                    discountAmount += (product.Price - priceAfterDiscount);

                    productDetails.Add(new ProductDiscountDetail
                    {
                        ProductId = product.Id,
                        OriginalPrice = product.Price,
                        PriceAfterDiscount = priceAfterDiscount
                    });
                }

                var newTotal = totalOriginal - discountAmount;

                return _response.Success(new ApplyDiscountResponse
                {
                    IsApplied = true,
                    DiscountAmount = discountAmount,
                    NewTotal = newTotal,
                    Products = productDetails
                }, "Discount applied successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error applying discount");
                return _response.ServerError<ApplyDiscountResponse>("Failed to apply discount.");
            }
        }


    }
}