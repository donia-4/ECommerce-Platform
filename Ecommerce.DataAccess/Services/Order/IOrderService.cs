// IOrderService.cs
using Ecommerce.Entities.DTO.Order;
using Ecommerce.Entities.Models;
using Ecommerce.Entities.Shared;
using Ecommerce.Entities.Shared.Bases;

namespace Ecommerce.DataAccess.Services.Order
{
    public interface IOrderService
    {
        // Admin methods
        Task<Response<OrderResponse>> AdminCreateOrderAsync(AdminCreateOrderRequest request);
        Task<Response<PaginatedList<OrderSummaryResponse>>> GetOrdersForAdminAsync(OrderQueryDto query);
        Task<Response<OrderResponse>> GetOrderByIdForAdminAsync(Guid orderId);
        Task<Response<OrderResponse>> UpdateOrderStatusAsync(Guid orderId, UpdateOrderStatusRequest request);
        Task<Response<bool>> DeleteOrderAsync(Guid orderId);

        // Buyer methods
        Task<Response<OrderResponse>> CreateOrderFromCartAsync(string buyerId, CreateOrderRequest request);
        Task<Response<PaginatedList<OrderSummaryResponse>>> GetOrdersForBuyerAsync(string buyerId, OrderQueryDto query);
        Task<Response<OrderResponse>> GetOrderByIdForBuyerAsync(string buyerId, Guid orderId);
        Task<Response<OrderResponse>> CancelOrderAsync(string buyerId, Guid orderId);
        Task<Response<bool>> DeleteOrderForBuyerAsync(string buyerId, Guid orderId);
    }
}