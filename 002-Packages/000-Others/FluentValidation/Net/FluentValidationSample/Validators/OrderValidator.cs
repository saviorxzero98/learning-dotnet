using FluentValidation;
using FluentValidationSample.Models;

namespace FluentValidationSample.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(p => p.Id).Must(id => !string.IsNullOrEmpty(id) &&
                                          Guid.TryParse(id, out var productId))
                              .WithName("訂單識別碼");
            RuleFor(p => p.Customer).NotNull()
                                    .SetValidator(new CustomerValidator())
                                    .WithName("顧客");
        }
    }
}
