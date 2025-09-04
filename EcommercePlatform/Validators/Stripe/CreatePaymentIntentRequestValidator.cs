using Ecommerce.Entities.DTO.Stripe;
using FluentValidation;

namespace Ecommerce.API.Validators.Stripe
{
    public class CreatePaymentIntentRequestValidator : AbstractValidator<CreatePaymentIntentRequest>
    {
        public CreatePaymentIntentRequestValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Matches("^[a-zA-Z]{3}$").WithMessage("Currency must be a valid 3-letter ISO code.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(255).WithMessage("Description must not exceed 255 characters.");
        }
    }
}
