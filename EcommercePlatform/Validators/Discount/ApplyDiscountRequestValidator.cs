namespace Ecommerce.API.Validators.Discount
{
    using Ecommerce.Entities.DTO.Discount;
    using FluentValidation;

    public class ApplyDiscountRequestValidator : AbstractValidator<ApplyDiscountRequest>
    {
        public ApplyDiscountRequestValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Discount code is required.");

            RuleFor(x => x.ProductIds)
                .NotNull().WithMessage("Product IDs are required.")
                .Must(list => list.Any()).WithMessage("At least one product is required.");
        }
    }

}
