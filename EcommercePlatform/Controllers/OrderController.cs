// OrderController.cs
using Ecommerce.DataAccess.Services.Order;
using Ecommerce.Entities.DTO.Order;
using Ecommerce.Entities.Shared;
using Ecommerce.Entities.Shared.Bases;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ResponseHandler _responseHandler;
        private readonly IValidator<CreateOrderRequest> _createOrderValidator;
        private readonly IValidator<AdminCreateOrderRequest> _adminCreateOrderValidator;
        private readonly IValidator<UpdateOrderStatusRequest> _updateOrderStatusValidator;
        private readonly IValidator<OrderQueryDto> _orderQueryValidator;

        public OrderController(
            IOrderService orderService,
            ResponseHandler responseHandler,
            IValidator<CreateOrderRequest> createOrderValidator,
            IValidator<AdminCreateOrderRequest> adminCreateOrderValidator,
            IValidator<UpdateOrderStatusRequest> updateOrderStatusValidator,
            IValidator<OrderQueryDto> orderQueryValidator)
        {
            _orderService = orderService;
            _responseHandler = responseHandler;
            _createOrderValidator = createOrderValidator;
            _adminCreateOrderValidator = adminCreateOrderValidator;
            _updateOrderStatusValidator = updateOrderStatusValidator;
            _orderQueryValidator = orderQueryValidator;
        }

        // Admin endpoints

        [HttpPost("admin/create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Response<OrderResponse>>> AdminCreateOrder([FromBody] AdminCreateOrderRequest request)
        {
            var validationResult = await _adminCreateOrderValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                string errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return StatusCode((int)_responseHandler.BadRequest<object>(errors).StatusCode,
                    _responseHandler.BadRequest<object>(errors));
            }

            var response = await _orderService.AdminCreateOrderAsync(request);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Response<PaginatedList<OrderSummaryResponse>>>> GetOrdersForAdmin(
            [FromQuery] OrderQueryDto query)
        {
            var validationResult = await _orderQueryValidator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                string errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return StatusCode((int)_responseHandler.BadRequest<object>(errors).StatusCode,
                    _responseHandler.BadRequest<object>(errors));
            }

            var response = await _orderService.GetOrdersForAdminAsync(query);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("admin/{orderId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Response<OrderResponse>>> GetOrderByIdForAdmin(Guid orderId)
        {
            var response = await _orderService.GetOrderByIdForAdminAsync(orderId);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut("admin/{orderId}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Response<OrderResponse>>> UpdateOrderStatus(Guid orderId, [FromBody] UpdateOrderStatusRequest request)
        {
            var validationResult = await _updateOrderStatusValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                string errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return StatusCode((int)_responseHandler.BadRequest<object>(errors).StatusCode,
                    _responseHandler.BadRequest<object>(errors));
            }

            var response = await _orderService.UpdateOrderStatusAsync(orderId, request);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("admin/{orderId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Response<bool>>> DeleteOrder(Guid orderId)
        {
            var response = await _orderService.DeleteOrderAsync(orderId);
            return StatusCode((int)response.StatusCode, response);
        }

        // Buyer endpoints

        [HttpPost("create")]
        [Authorize(Roles = "Buyer")]
        public async Task<ActionResult<Response<OrderResponse>>> CreateOrderFromCart([FromBody] CreateOrderRequest request)
        {
            var validationResult = await _createOrderValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                string errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return StatusCode((int)_responseHandler.BadRequest<object>(errors).StatusCode,
                    _responseHandler.BadRequest<object>(errors));
            }

            var buyerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var response = await _orderService.CreateOrderFromCartAsync(buyerId, request);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet]
        [Authorize(Roles = "Buyer")]
        public async Task<ActionResult<Response<PaginatedList<OrderSummaryResponse>>>> GetOrdersForBuyer(
            [FromQuery] OrderQueryDto query)
        {
            var validationResult = await _orderQueryValidator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                string errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return StatusCode((int)_responseHandler.BadRequest<object>(errors).StatusCode,
                    _responseHandler.BadRequest<object>(errors));
            }

            var buyerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var response = await _orderService.GetOrdersForBuyerAsync(buyerId, query);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{orderId}")]
        [Authorize(Roles = "Buyer")]
        public async Task<ActionResult<Response<OrderResponse>>> GetOrderByIdForBuyer(Guid orderId)
        {
            var buyerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var response = await _orderService.GetOrderByIdForBuyerAsync(buyerId, orderId);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut("{orderId}/cancel")]
        [Authorize(Roles = "Buyer")]
        public async Task<ActionResult<Response<OrderResponse>>> CancelOrder(Guid orderId)
        {
            var buyerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var response = await _orderService.CancelOrderAsync(buyerId, orderId);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("{orderId}")]
        [Authorize(Roles = "Buyer")]
        public async Task<ActionResult<Response<bool>>> DeleteOrderForBuyer(Guid orderId)
        {
            var buyerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var response = await _orderService.DeleteOrderForBuyerAsync(buyerId, orderId);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}