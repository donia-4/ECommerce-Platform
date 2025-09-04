using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Entities.DTO.Discount;
using Ecommerce.Entities.Shared;
using Ecommerce.Entities.Shared.Bases;
namespace Ecommerce.DataAccess.Services.Discount
{
    public interface IDiscountService
    {
        Task<Response<Guid>> CreateAsync(CreateDiscountRequest request);
        Task<Response<Guid>> UpdateAsync(Guid id, UpdateDiscountRequest request);
        Task<Response<GetDiscountResponse>> GetByIdAsync(Guid id);
        Task<Response<List<GetDiscountResponse>>> GetAllAsync(bool? onlyActive = null);
        Task<Response<bool>> DeactivateAsync(Guid id);
        Task<Response<bool>> DeleteAsync(Guid id);
        Task<Response<PaginatedList<GetDiscountResponse>>> GetPaginatedAsync(GetDiscountsQuery query);
        Task<Response<List<GetActiveDiscountsResponse>>> GetActiveDiscountsAsync(GetActiveDiscountsRequest request);
        Task<Response<ApplyDiscountResponse>> ApplyDiscountAsync(ApplyDiscountRequest request);


    }
}
