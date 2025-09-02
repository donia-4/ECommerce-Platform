namespace Ecommerce.API.Validators.Discount
{
    using Ecommerce.Entities.DTO.Discount;
    using FluentValidation;

    public class GetActiveDiscountsRequestValidator : AbstractValidator<GetActiveDiscountsRequest>
    {
        public GetActiveDiscountsRequestValidator()
        {
            RuleFor(x => x)
            .Must(x =>
                (x.ProductIds?.Any() == true) ||
                (x.CategoryIds?.Any() == true) ||
                (x.ProductIds == null && x.CategoryIds == null)
            )
            .WithMessage("Provide at least one product or one category, or leave both empty to get all discounts.");
        }
    }

}
