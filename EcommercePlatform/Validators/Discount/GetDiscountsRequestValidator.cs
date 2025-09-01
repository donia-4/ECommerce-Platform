using Ecommerce.Entities.DTO.Discount;
using FluentValidation;
using Ecommerce.Utilities.Enums;
namespace Ecommerce.API.Validators.Discount
{
    public class GetDiscountsQueryValidator : AbstractValidator<GetDiscountsQuery>
    {
        public GetDiscountsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("Page number must be greater than 0.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.");

            RuleFor(x => x.Status)
                .Must(value => string.IsNullOrEmpty(value) || Enum.TryParse<DiscountStatus>(value, true, out _))
                .WithMessage("Invalid status. Allowed values: Active, Expired, NoDiscount.");
        }
    }
}
