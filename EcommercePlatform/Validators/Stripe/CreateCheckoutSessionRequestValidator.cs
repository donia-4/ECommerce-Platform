using Ecommerce.Entities.DTO.Stripe;
using FluentValidation;

namespace Ecommerce.API.Validators.Stripe
{
    public class CreateCheckoutSessionRequestValidator : AbstractValidator<CreateCheckoutSessionRequest>
    {
        public CreateCheckoutSessionRequestValidator()
        {
            RuleFor(x => x.ProductNames)
                .NotNull().WithMessage("Product names are required.")
                .Must(p => p.Count > 0).WithMessage("At least one product is required.");

            RuleFor(x => x.UnitAmounts)
                .NotNull().WithMessage("Unit amounts are required.")
                .Must(u => u.Count > 0).WithMessage("At least one unit amount is required.")
                .Must(u => u.All(amount => amount > 0)).WithMessage("Unit amounts must be greater than 0.");

            RuleFor(x => x.Quantities)
                .NotNull().WithMessage("Quantities are required.")
                .Must(q => q.Count > 0).WithMessage("At least one quantity is required.")
                .Must(q => q.All(quantity => quantity > 0)).WithMessage("Quantities must be greater than 0.");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Matches("^[a-zA-Z]{3}$").WithMessage("Currency must be a valid 3-letter ISO code.");

            RuleFor(x => x.SuccessUrl)
                .NotEmpty().WithMessage("Success URL is required.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("Success URL must be a valid URL.");

            RuleFor(x => x.CancelUrl)
                .NotEmpty().WithMessage("Cancel URL is required.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("Cancel URL must be a valid URL.");

            RuleFor(x => x)
                .Must(x => x.ProductNames.Count == x.UnitAmounts.Count && x.ProductNames.Count == x.Quantities.Count)
                .WithMessage("ProductNames, UnitAmounts, and Quantities must have the same number of elements.");
        }
    }
}
