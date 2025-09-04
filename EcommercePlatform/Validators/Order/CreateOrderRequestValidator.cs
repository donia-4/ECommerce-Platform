using Ecommerce.Entities.DTO.Order;
using FluentValidation;

namespace Ecommerce.API.Validators.Order
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.ShippingAddress).NotEmpty().MaximumLength(500);
            RuleFor(x => x.ShipPostalCode).MaximumLength(20);
        }
    }
}
