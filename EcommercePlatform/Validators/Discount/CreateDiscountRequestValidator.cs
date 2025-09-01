using Ecommerce.Entities.DTO.Discount;
using FluentValidation;
using Ecommerce.Utilities.Enums;

namespace Ecommerce.API.Validators.Discount
{
    public class CreateDiscountRequestValidator : AbstractValidator<CreateDiscountRequest>
    {
        public CreateDiscountRequestValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MaximumLength(50).WithMessage("Code max length is 50.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type is required.")
                .Must(value => Enum.TryParse<DiscountType>(value, true, out _))
                .WithMessage("Invalid discount type. Allowed values: Percentage, FixedAmount.");

            When(x => x.Type.Equals("Percentage", StringComparison.OrdinalIgnoreCase), () =>
            {
                RuleFor(x => x.Value)
                    .GreaterThan(0).WithMessage("Percentage must be > 0.")
                    .LessThanOrEqualTo(100).WithMessage("Percentage must be ≤ 100.");
            });

            When(x => x.Type.Equals("FixedAmount", StringComparison.OrdinalIgnoreCase), () =>
            {
                RuleFor(x => x.Value)
                    .GreaterThan(0).WithMessage("Amount must be > 0.");
            });

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate).WithMessage("StartDate must be before EndDate.");

            RuleFor(x => x)
                .Must(x => (x.ProductIds?.Count > 0) || (x.CategoryIds?.Count > 0))
                .WithMessage("Select at least one product or one category.");

            RuleFor(x => x.ProductIds)
                .Must(list => list == null || list.Distinct().Count() == list.Count)
                .WithMessage("Duplicate product IDs are not allowed.");

            RuleFor(x => x.CategoryIds)
                .Must(list => list == null || list.Distinct().Count() == list.Count)
                .WithMessage("Duplicate category IDs are not allowed.");
        }
    }
}
