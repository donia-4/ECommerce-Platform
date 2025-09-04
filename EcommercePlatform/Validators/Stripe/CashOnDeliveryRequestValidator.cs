using Ecommerce.Entities.DTO.Stripe;
using FluentValidation;

namespace Ecommerce.API.Validators
{
    public class CashOnDeliveryRequestValidator : AbstractValidator<CashOnDeliveryRequest>
    {
        public CashOnDeliveryRequestValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("Order ID is required")
                .NotEqual(Guid.Empty).WithMessage("Valid Order ID is required");

            RuleFor(x => x.BuyerId)
                .NotEmpty().WithMessage("Buyer ID is required")
                .NotEqual(Guid.Empty).WithMessage("Valid Buyer ID is required");

            RuleFor(x => x.TotalAmount)
                .GreaterThan(0).WithMessage("Total amount must be greater than 0")
                .LessThan(1000000).WithMessage("Total amount cannot exceed 1,000,000");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required")
                .Length(3).WithMessage("Currency must be 3 characters")
                .Matches("^[A-Z]{3}$").WithMessage("Currency must be in ISO format (e.g., EGP, USD)");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("At least one item is required")
                .Must(items => items != null && items.Any()).WithMessage("Items list cannot be empty");

            RuleForEach(x => x.Items).SetValidator(new OrderItemDtoValidator());
        }
    }

    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Product ID is required")
                .NotEqual(Guid.Empty).WithMessage("Valid Product ID is required");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0")
                .LessThanOrEqualTo(1000).WithMessage("Quantity cannot exceed 1000");
        }
    }
}