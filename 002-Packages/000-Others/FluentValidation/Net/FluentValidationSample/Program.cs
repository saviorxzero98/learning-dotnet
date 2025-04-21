using FluentValidationSample.Models;
using FluentValidationSample.Validators;

namespace FluentValidationSample
{
    public class Program
    {
        static void Main(string[] args)
        {
            var products = new List<Product>()
            {
                new Product(),
                new Product()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Test",
                    Price = 100,
                    Stock = 1,
                    Timestamp = DateTime.UtcNow
                }
            };

            var orders = new List<Order>()
            {
                new Order()
                {
                    Id = Guid.NewGuid().ToString()
                },
                new Order()
                {
                    Id = Guid.NewGuid().ToString(),
                    Customer = new Customer()
                },
                new Order()
                {
                    Id = Guid.NewGuid().ToString(),
                    Customer = new Customer()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Test"
                    }
                }
            };


            ValidateProducts(products);
            ValidateOrders(orders);
        }


        static void ValidateProducts(List<Product> products)
        {
            var vaildator = new ProductValidator();

            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"驗證產品 {i+1}");

                var product = products[i];  
                var result = vaildator.Validate(product);

                if (result.IsValid)
                {
                    Console.WriteLine($"產品 {i + 1} 是合法的");
                }
                else
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage.Replace("'", string.Empty)).ToList();
                    Console.WriteLine(string.Join("\n", errorMessages));
                }

                Console.WriteLine();
            }
        }
    
        static void ValidateOrders(List<Order> orders)
        {
            var vaildator = new OrderValidator();

            for (int i = 0; i < orders.Count; i++)
            {
                Console.WriteLine($"驗證訂單 {i + 1}");

                var order = orders[i];
                var result = vaildator.Validate(order);

                if (result.IsValid)
                {
                    Console.WriteLine($"訂單 {i + 1} 是合法的");
                }
                else
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage.Replace("'", string.Empty)).ToList();
                    Console.WriteLine(string.Join("\n", errorMessages));
                }

                Console.WriteLine();
            }
        }
    }
}
