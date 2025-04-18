using FluentValidationSample.Models;
using FluentValidationSample.Validators;

namespace FluentValidationSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DemoValid();
        }

        static void DemoValid()
        {
            var vaildator = new ProductValidator();

            var invalidProduct = new Product();

            var validResult1 = vaildator.Validate(invalidProduct);
        }
    }
}
