namespace Ecommerce.API.Validators
{
    using Ecommerce.Entities.DTO.Account.Auth.Register;
    using FluentValidation;
    using System;

    public class RegisterBuyerRequestValidator : AbstractValidator<RegisterBuyerRequest>
    {
        public RegisterBuyerRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(200).WithMessage("Full name max length is 200.");

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.UtcNow).WithMessage("Birth date must be in the past.");

        }
    }

}
