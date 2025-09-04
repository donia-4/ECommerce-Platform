using Ecommerce.Entities.DTO.Order;
using Ecommerce.Utilities.Enums;
using FluentValidation;

public class OrderQueryDtoValidator : AbstractValidator<OrderQueryDto>
{
    public OrderQueryDtoValidator()
    {
        RuleFor(x => x.SearchTerm)
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.SearchTerm))
            .WithMessage("Search term cannot exceed 100 characters.");

        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
            .WithMessage("Page size must be between 1 and 100.");

        // Validate Status if provided as string
        RuleFor(x => x.Status)
            .Must(BeAValidOrderStatus)
            .When(x => !string.IsNullOrEmpty(x.Status))
            .WithMessage("Status must be a valid order status. Valid values are: " +
                         string.Join(", ", Enum.GetNames(typeof(OrderStatus))));
    }

    private bool BeAValidOrderStatus(string status)
    {
        return Enum.TryParse(typeof(OrderStatus), status, true, out _);
    }
}