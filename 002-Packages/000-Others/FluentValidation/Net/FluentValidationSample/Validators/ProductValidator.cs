using FluentValidation;
using FluentValidationSample.Models;

namespace FluentValidationSample.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Id).Must(id => !string.IsNullOrEmpty(id) &&
                                          Guid.TryParse(id, out var productId))
                              .WithMessage("產品 ID 必須是一個 GUID");
            RuleFor(p => p.Name).NotEmpty().WithName("產品名稱");
            RuleFor(p => p.Price).GreaterThan(0).WithName("產品價格");
            RuleFor(p => p.Stock).GreaterThanOrEqualTo(0).WithName("產品庫存");
        }
    }
}
