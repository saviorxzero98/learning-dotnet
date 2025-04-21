using FluentValidation;
using FluentValidationSample.Models;

namespace FluentValidationSample.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(p => p.Id).Must(id => !string.IsNullOrEmpty(id) &&
                                          Guid.TryParse(id, out var productId))
                              .WithName("顧客識別碼");
            RuleFor(p => p.Name).NotEmpty().WithName("顧客名稱");
        }
    }
}
