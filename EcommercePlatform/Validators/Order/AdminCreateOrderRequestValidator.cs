using Ecommerce.Entities.DTO.Order;
using FluentValidation;

namespace Ecommerce.API.Validators.Order
{
    public class AdminCreateOrderRequestValidator : AbstractValidator<AdminCreateOrderRequest>
    {
        public AdminCreateOrderRequestValidator()
        {
            RuleFor(x => x.BuyerId).NotEmpty();
            RuleFor(x => x.ShippingAddress).NotEmpty().MaximumLength(500);
            RuleFor(x => x.ShipPostalCode).MaximumLength(20);
            RuleFor(x => x.Items).NotEmpty();
            RuleForEach(x => x.Items).SetValidator(new OrderItemRequestValidator());
        }
    }
}
