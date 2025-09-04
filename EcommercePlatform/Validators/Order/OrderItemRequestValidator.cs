using Ecommerce.Entities.DTO.Order;
using FluentValidation;

namespace Ecommerce.API.Validators.Order
{
    public class OrderItemRequestValidator : AbstractValidator<OrderItemRequest>
    {
        public OrderItemRequestValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
