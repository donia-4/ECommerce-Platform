using Ecommerce.DataAccess.ApplicationContext;
using Ecommerce.Entities.DTO.Discount;
using Ecommerce.Utilities.Enums;
using FluentValidation;

namespace Ecommerce.API.Validators.Discount
{
    public class UpdateDiscountRequestValidator : AbstractValidator<UpdateDiscountRequest>
    {
        private readonly ApplicationDbContext _db;

        public UpdateDiscountRequestValidator(ApplicationDbContext db)
        {
            _db = db;

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

            RuleFor(x => x.ProductIds)
                .Must(ids => ids == null || ids.Count == 0 ||
                    _db.Products.Count(p => ids.Contains(p.Id)) == ids.Count)
                .WithMessage("One or more product IDs do not exist.");

            RuleFor(x => x.CategoryIds)
                .Must(ids => ids == null || ids.Count == 0 ||
                    _db.Categories.Count(c => ids.Contains(c.Id)) == ids.Count)
                .WithMessage("One or more category IDs do not exist.");

            When(x => x.Type.Equals("FixedAmount", StringComparison.OrdinalIgnoreCase) && x.ProductIds?.Count > 0, () =>
            {
                RuleFor(x => x)
                    .Must(req =>
                    {
                        var prices = _db.Products
                            .Where(p => req.ProductIds.Contains(p.Id))
                            .Select(p => p.Price)
                            .ToList();

                        var minPrice = prices.Any() ? prices.Min() : decimal.MaxValue;

                        return req.Value < minPrice;
                    })
                    .WithMessage("Fixed amount must be less than the minimum price among selected products.");
            });
        }
    }
}
