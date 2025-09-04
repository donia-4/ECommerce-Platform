// OrderService.cs
using Azure.Core;
using Ecommerce.DataAccess.ApplicationContext;
using Ecommerce.Entities.DTO.Order;
using Ecommerce.Entities.Models;
using Ecommerce.Entities.Shared;
using Ecommerce.Entities.Shared.Bases;
using Ecommerce.Utilities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.DataAccess.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly ResponseHandler _responseHandler;
        private readonly ILogger<OrderService> _logger;

        public OrderService(
            ApplicationDbContext context,
            ResponseHandler responseHandler,
            ILogger<OrderService> logger)
        {
            _context = context;
            _responseHandler = responseHandler;
            _logger = logger;
        }

        public async Task<Response<OrderResponse>> AdminCreateOrderAsync(AdminCreateOrderRequest request)
        {
            _logger.LogInformation("Admin creating order for BuyerId: {BuyerId}", request.BuyerId);

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Check if buyer exists
                var buyer = await _context.Buyers.FindAsync(request.BuyerId);
                if (buyer == null)
                {
                    _logger.LogWarning("Buyer not found. BuyerId: {BuyerId}", request.BuyerId);
                    return _responseHandler.NotFound<OrderResponse>("Buyer not found.");
                }

                var order = new Entities.Models.Order
                {
                    Id = Guid.NewGuid(),
                    BuyerId = request.BuyerId,
                    CreatedAt = DateTime.UtcNow,
                    Status = OrderStatus.Pending, // Default status
                    ShippingAddress = request.ShippingAddress,
                    ShipPostalCode = request.ShipPostalCode,
                    Items = new List<OrderItem>()
                };

                decimal subtotal = 0;

                foreach (var itemRequest in request.Items)
                {
                    var product = await _context.Products
                        .Include(p => p.DiscountLinks)
                        .ThenInclude(dl => dl.Discount)
                        .FirstOrDefaultAsync(p => p.Id == itemRequest.ProductId);

                    if (product == null)
                    {
                        _logger.LogWarning("Product not found. ProductId: {ProductId}", itemRequest.ProductId);
                        return _responseHandler.NotFound<OrderResponse>($"Product not found: {itemRequest.ProductId}");
                    }

                    if (product.Stock < itemRequest.Quantity)
                    {
                        _logger.LogWarning("Insufficient stock for ProductId: {ProductId}. Requested: {Quantity}, Available: {Stock}",
                            itemRequest.ProductId, itemRequest.Quantity, product.Stock);
                        return _responseHandler.BadRequest<OrderResponse>($"Insufficient stock for product: {product.Name}");
                    }

                    decimal unitPrice = product.Price;
                    decimal itemDiscount = 0;

                    var activeDiscount = product.DiscountLinks
                        .Where(dl => dl.Discount.IsActive &&
                                    dl.Discount.StartDate <= DateTime.UtcNow &&
                                    dl.Discount.EndDate >= DateTime.UtcNow)
                        .OrderByDescending(dl => dl.Discount.Value)
                        .FirstOrDefault();

                    if (activeDiscount != null)
                    {
                        if (activeDiscount.Discount.Type == DiscountType.Percentage)
                        {
                            itemDiscount = unitPrice * (activeDiscount.Discount.Value / 100);
                        }
                        else // Fixed amount
                        {
                            itemDiscount = activeDiscount.Discount.Value;
                        }
                        unitPrice -= itemDiscount;
                    }

                    var orderItem = new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        ProductId = product.Id,
                        Quantity = itemRequest.Quantity,
                        UnitPrice = unitPrice,
                        Subtotal = unitPrice * itemRequest.Quantity,
                        CreatedAt = DateTime.UtcNow
                    };

                    order.Items.Add(orderItem);
                    subtotal += orderItem.Subtotal;

                    // Reduce stock
                    product.Stock -= itemRequest.Quantity;
                    _context.Products.Update(product);
                }

                order.Subtotal = subtotal;
                order.Total = subtotal; // For now, no tax or shipping

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var response = await BuildOrderResponse(order.Id);
                _logger.LogInformation("Order created successfully. OrderId: {OrderId}", order.Id);
                return _responseHandler.Success(response, "Order created successfully.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating order for BuyerId: {BuyerId}", request.BuyerId);
                return _responseHandler.InternalServerError<OrderResponse>("An error occurred while creating order.");
            }
        }

        public async Task<Response<OrderResponse>> CreateOrderFromCartAsync(string buyerId, CreateOrderRequest request)
        {
            _logger.LogInformation("Creating order from cart for BuyerId: {BuyerId}", buyerId);

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Get buyer's cart
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                    .ThenInclude(p => p.DiscountLinks)
                    .ThenInclude(dl => dl.Discount)
                    .FirstOrDefaultAsync(c => c.BuyerId == buyerId);

                if (cart == null || cart.CartItems == null || !cart.CartItems.Any())
                {
                    _logger.LogWarning("Cart is empty for BuyerId: {BuyerId}", buyerId);
                    return _responseHandler.BadRequest<OrderResponse>("Cart is empty.");
                }

                var order = new Entities.Models.Order
                {
                    Id = Guid.NewGuid(),
                    BuyerId = buyerId,
                    CreatedAt = DateTime.UtcNow,
                    Status = OrderStatus.Pending, // Default status
                    ShippingAddress = request.ShippingAddress,
                    ShipPostalCode = request.ShipPostalCode,
                    Items = new List<OrderItem>()
                };

                decimal subtotal = 0;

                // Process each cart item
                foreach (var cartItem in cart.CartItems)
                {
                    var product = cartItem.Product;

                    if (product.Stock < cartItem.Quantity)
                    {
                        _logger.LogWarning("Insufficient stock for ProductId: {ProductId}. Requested: {Quantity}, Available: {Stock}",
                            product.Id, cartItem.Quantity, product.Stock);
                        return _responseHandler.BadRequest<OrderResponse>($"Insufficient stock for product: {product.Name}");
                    }

                    // Calculate price with discount if any
                    decimal unitPrice = product.Price;
                    decimal itemDiscount = 0;

                    // Check for active discounts
                    var activeDiscount = product.DiscountLinks
                        .Where(dl => dl.Discount.IsActive &&
                                    dl.Discount.StartDate <= DateTime.UtcNow &&
                                    dl.Discount.EndDate >= DateTime.UtcNow)
                        .OrderByDescending(dl => dl.Discount.Value)
                        .FirstOrDefault();

                    if (activeDiscount != null)
                    {
                        if (activeDiscount.Discount.Type == DiscountType.Percentage)
                        {
                            itemDiscount = unitPrice * (activeDiscount.Discount.Value / 100);
                        }
                        else // Fixed amount
                        {
                            itemDiscount = activeDiscount.Discount.Value;
                        }
                        unitPrice -= itemDiscount;
                    }

                    var orderItem = new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        ProductId = product.Id,
                        Quantity = cartItem.Quantity,
                        UnitPrice = unitPrice,
                        Subtotal = unitPrice * cartItem.Quantity,
                        CreatedAt = DateTime.UtcNow
                    };

                    order.Items.Add(orderItem);
                    subtotal += orderItem.Subtotal;

                    // Reduce stock
                    product.Stock -= cartItem.Quantity;
                    _context.Products.Update(product);
                }

                order.Subtotal = subtotal;
                order.Total = subtotal; // For now, no tax or shipping

                _context.Orders.Add(order);

                // Clear the cart
                _context.CartItems.RemoveRange(cart.CartItems);
                cart.UpdatedAt = DateTime.UtcNow;
                _context.Carts.Update(cart);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var response = await BuildOrderResponse(order.Id);
                _logger.LogInformation("Order created successfully from cart. OrderId: {OrderId}", order.Id);
                return _responseHandler.Success(response, "Order created successfully.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating order from cart for BuyerId: {BuyerId}", buyerId);
                return _responseHandler.InternalServerError<OrderResponse>("An error occurred while creating order.");
            }
        }

        public async Task<Response<PaginatedList<OrderSummaryResponse>>> GetOrdersForAdminAsync(OrderQueryDto query)
        {
            _logger.LogInformation("Retrieving orders for admin with query: {@Query}", query);

            try
            {
                var dbQuery = _context.Orders
                    .Include(o => o.Buyer)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(query.Status))
                {
                    if (Enum.TryParse<OrderStatus>(query.Status, true, out var statusFilter))
                    {
                        dbQuery = dbQuery.Where(o => o.Status == statusFilter);
                    }
                    else
                    {
                        _logger.LogWarning("Invalid status filter provided: {Status}", query.Status);
                        return _responseHandler.BadRequest<PaginatedList<OrderSummaryResponse>>(
                            $"Invalid status filter: {query.Status}. Valid values are: " +
                            string.Join(", ", Enum.GetNames(typeof(OrderStatus))));
                    }
                }

                if (!string.IsNullOrEmpty(query.SearchTerm))
                {
                    dbQuery = dbQuery.Where(o =>
                        o.Id.ToString().Contains(query.SearchTerm) ||
                        o.Buyer.FullName.Contains(query.SearchTerm));
                }

                // Create paginated list
                var ordersQuery = dbQuery
                    .OrderByDescending(o => o.CreatedAt)
                    .Select(o => new OrderSummaryResponse
                    {
                        Id = o.Id,
                        BuyerName = o.Buyer.FullName,
                        CreatedAt = o.CreatedAt,
                        Status = o.Status.ToString(), // Convert enum to string
                        Total = o.Total
                    });

                var paginatedList = await PaginatedList<OrderSummaryResponse>.CreateAsync(
                    ordersQuery, query.PageNumber, query.PageSize);

                return _responseHandler.Success(paginatedList, "Orders retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving orders for admin");
                return _responseHandler.InternalServerError<PaginatedList<OrderSummaryResponse>>("An error occurred while retrieving orders.");
            }
        }

        public async Task<Response<PaginatedList<OrderSummaryResponse>>> GetOrdersForBuyerAsync(string buyerId, OrderQueryDto query)
        {
            _logger.LogInformation("Retrieving orders for BuyerId: {BuyerId} with query: {@Query}", buyerId, query);

            try
            {
                var dbQuery = _context.Orders
                    .Where(o => o.BuyerId == buyerId)
                    .AsQueryable();

                // Apply filters - handle string status
                if (!string.IsNullOrEmpty(query.Status))
                {
                    if (Enum.TryParse<OrderStatus>(query.Status, true, out var statusFilter))
                    {
                        dbQuery = dbQuery.Where(o => o.Status == statusFilter);
                    }
                    else
                    {
                        _logger.LogWarning("Invalid status filter provided: {Status}", query.Status);
                        return _responseHandler.BadRequest<PaginatedList<OrderSummaryResponse>>(
                            $"Invalid status filter: {query.Status}. Valid values are: " +
                            string.Join(", ", Enum.GetNames(typeof(OrderStatus))));
                    }
                }

                // Create paginated list
                var ordersQuery = dbQuery
                    .OrderByDescending(o => o.CreatedAt)
                    .Select(o => new OrderSummaryResponse
                    {
                        Id = o.Id,
                        BuyerName = "You", // For buyer view, we don't need to show their own name
                        CreatedAt = o.CreatedAt,
                        Status = o.Status.ToString(), // Convert enum to string
                        Total = o.Total
                    });

                var paginatedList = await PaginatedList<OrderSummaryResponse>.CreateAsync(
                    ordersQuery, query.PageNumber, query.PageSize);

                return _responseHandler.Success(paginatedList, "Orders retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving orders for BuyerId: {BuyerId}", buyerId);
                return _responseHandler.InternalServerError<PaginatedList<OrderSummaryResponse>>("An error occurred while retrieving orders.");
            }
        }

        public async Task<Response<OrderResponse>> GetOrderByIdForAdminAsync(Guid orderId)
        {
            _logger.LogInformation("Retrieving order details for admin. OrderId: {OrderId}", orderId);

            try
            {
                var order = await _context.Orders
                    .Include(o => o.Buyer)
                    .Include(o => o.Items)
                    .ThenInclude(oi => oi.Product)
                    .FirstOrDefaultAsync(o => o.Id == orderId);

                if (order == null)
                {
                    _logger.LogWarning("Order not found. OrderId: {OrderId}", orderId);
                    return _responseHandler.NotFound<OrderResponse>("Order not found.");
                }

                var response = await BuildOrderResponse(orderId);
                return _responseHandler.Success(response, "Order details retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving order details for admin. OrderId: {OrderId}", orderId);
                return _responseHandler.InternalServerError<OrderResponse>("An error occurred while retrieving order details.");
            }
        }

        public async Task<Response<OrderResponse>> GetOrderByIdForBuyerAsync(string buyerId, Guid orderId)
        {
            _logger.LogInformation("Retrieving order details for BuyerId: {BuyerId}, OrderId: {OrderId}", buyerId, orderId);

            try
            {
                var order = await _context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(oi => oi.Product)
                    .FirstOrDefaultAsync(o => o.Id == orderId && o.BuyerId == buyerId);

                if (order == null)
                {
                    _logger.LogWarning("Order not found. OrderId: {OrderId}, BuyerId: {BuyerId}", orderId, buyerId);
                    return _responseHandler.NotFound<OrderResponse>("Order not found.");
                }

                var response = await BuildOrderResponse(orderId);
                return _responseHandler.Success(response, "Order details retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving order details for BuyerId: {BuyerId}, OrderId: {OrderId}", buyerId, orderId);
                return _responseHandler.InternalServerError<OrderResponse>("An error occurred while retrieving order details.");
            }
        }

        public async Task<Response<OrderResponse>> UpdateOrderStatusAsync(Guid orderId, UpdateOrderStatusRequest request)
        {
            _logger.LogInformation("Updating order status. OrderId: {OrderId}, NewStatus: {Status}", orderId, request.Status);

            try
            {
                // Parse string status to enum
                if (!Enum.TryParse<OrderStatus>(request.Status, true, out var newStatus))
                {
                    _logger.LogWarning("Invalid status provided: {Status}", request.Status);
                    return _responseHandler.BadRequest<OrderResponse>(
                        $"Invalid status: {request.Status}. Valid values are: " +
                        string.Join(", ", Enum.GetNames(typeof(OrderStatus))));
                }

                var order = await _context.Orders.FindAsync(orderId);
                if (order == null)
                {
                    _logger.LogWarning("Order not found. OrderId: {OrderId}", orderId);
                    return _responseHandler.NotFound<OrderResponse>("Order not found.");
                }

                // Validate status transition
                if (!IsValidStatusTransition(order.Status, newStatus))
                {
                    _logger.LogWarning("Invalid status transition from {CurrentStatus} to {NewStatus}", order.Status, newStatus);
                    return _responseHandler.BadRequest<OrderResponse>($"Cannot change status from {order.Status} to {newStatus}.");
                }

                order.Status = newStatus;
                order.UpdatedAt = DateTime.UtcNow;

                _context.Orders.Update(order);
                await _context.SaveChangesAsync();

                // TODO: Notify buyer about status change (email, notification, etc.)

                var response = await BuildOrderResponse(orderId);
                _logger.LogInformation("Order status updated successfully. OrderId: {OrderId}", orderId);
                return _responseHandler.Success(response, "Order status updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating order status. OrderId: {OrderId}", orderId);
                return _responseHandler.InternalServerError<OrderResponse>("An error occurred while updating order status.");
            }
        }

        public async Task<Response<OrderResponse>> CancelOrderAsync(string buyerId, Guid orderId)
        {
            _logger.LogInformation("Canceling order. BuyerId: {BuyerId}, OrderId: {OrderId}", buyerId, orderId);

            try
            {
                var order = await _context.Orders
                    .Include(o => o.Items)
                    .FirstOrDefaultAsync(o => o.Id == orderId && o.BuyerId == buyerId);

                if (order == null)
                {
                    _logger.LogWarning("Order not found. OrderId: {OrderId}, BuyerId: {BuyerId}", orderId, buyerId);
                    return _responseHandler.NotFound<OrderResponse>("Order not found.");
                }

                // Only pending orders can be canceled
                if (order.Status != OrderStatus.Pending)
                {
                    _logger.LogWarning("Cannot cancel order with status: {Status}", order.Status);
                    return _responseHandler.BadRequest<OrderResponse>($"Cannot cancel order with status: {order.Status}");
                }

                order.Status = OrderStatus.Canceled;
                order.UpdatedAt = DateTime.UtcNow;

                // Restore product stock
                foreach (var item in order.Items)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product != null)
                    {
                        product.Stock += item.Quantity;
                        _context.Products.Update(product);
                    }
                }

                _context.Orders.Update(order);
                await _context.SaveChangesAsync();

                // TODO: Notify buyer about cancellation

                var response = await BuildOrderResponse(orderId);
                _logger.LogInformation("Order canceled successfully. OrderId: {OrderId}", orderId);
                return _responseHandler.Success(response, "Order canceled successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while canceling order. BuyerId: {BuyerId}, OrderId: {OrderId}", buyerId, orderId);
                return _responseHandler.InternalServerError<OrderResponse>("An error occurred while canceling order.");
            }
        }

        public async Task<Response<bool>> DeleteOrderAsync(Guid orderId)
        {
            _logger.LogInformation("Deleting order. OrderId: {OrderId}", orderId);

            try
            {
                var order = await _context.Orders.FindAsync(orderId);
                if (order == null)
                {
                    _logger.LogWarning("Order not found. OrderId: {OrderId}", orderId);
                    return _responseHandler.NotFound<bool>("Order not found.");
                }

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Order deleted successfully. OrderId: {OrderId}", orderId);
                return _responseHandler.Success(true, "Order deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting order. OrderId: {OrderId}", orderId);
                return _responseHandler.InternalServerError<bool>("An error occurred while deleting order.");
            }
        }

        public async Task<Response<bool>> DeleteOrderForBuyerAsync(string buyerId, Guid orderId)
        {
            _logger.LogInformation("Deleting order for buyer. BuyerId: {BuyerId}, OrderId: {OrderId}", buyerId, orderId);

            try
            {
                var order = await _context.Orders
                    .FirstOrDefaultAsync(o => o.Id == orderId && o.BuyerId == buyerId);

                if (order == null)
                {
                    _logger.LogWarning("Order not found. OrderId: {OrderId}, BuyerId: {BuyerId}", orderId, buyerId);
                    return _responseHandler.NotFound<bool>("Order not found.");
                }

                // Only canceled orders can be deleted by buyer
                if (order.Status != OrderStatus.Canceled)
                {
                    _logger.LogWarning("Cannot delete order with status: {Status}", order.Status);
                    return _responseHandler.BadRequest<bool>($"Only canceled orders can be deleted. Current status: {order.Status}");
                }

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Order deleted successfully. OrderId: {OrderId}", orderId);
                return _responseHandler.Success(true, "Order deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting order. BuyerId: {BuyerId}, OrderId: {OrderId}", buyerId, orderId);
                return _responseHandler.InternalServerError<bool>("An error occurred while deleting order.");
            }
        }

        private async Task<OrderResponse> BuildOrderResponse(Guid orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Buyer)
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null) return null;

            return new OrderResponse
            {
                Id = order.Id,
                BuyerId = order.BuyerId,
                CreatedAt = order.CreatedAt,
                Status = order.Status.ToString(), // Convert enum to string
                Subtotal = order.Subtotal,
                DiscountAmount = order.DiscountAmount,
                Total = order.Total,
                ShippingAddress = order.ShippingAddress,
                ShipPostalCode = order.ShipPostalCode,
                Items = order.Items.Select(oi => new OrderItemResponse
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    Subtotal = oi.Subtotal
                }).ToList()
            };
        }

        private bool IsValidStatusTransition(OrderStatus currentStatus, OrderStatus newStatus)
        {
            // Define valid status transitions
            var validTransitions = new Dictionary<OrderStatus, List<OrderStatus>>
            {
                { OrderStatus.Pending, new List<OrderStatus> { OrderStatus.Processing, OrderStatus.Canceled } },
                { OrderStatus.Processing, new List<OrderStatus> { OrderStatus.Shipped, OrderStatus.Canceled } },
                { OrderStatus.Shipped, new List<OrderStatus> { OrderStatus.Delivered } },
                { OrderStatus.Delivered, new List<OrderStatus>() }, // Final state
                { OrderStatus.Canceled, new List<OrderStatus>() } // Final state
            };

            return validTransitions.ContainsKey(currentStatus) &&
                   validTransitions[currentStatus].Contains(newStatus);
        }

        // Helper method to parse string status to enum
        private OrderStatus? ParseOrderStatus(string statusString)
        {
            if (string.IsNullOrEmpty(statusString))
                return null;

            if (Enum.TryParse<OrderStatus>(statusString, true, out var status))
                return status;

            return null;
        }
    }
}