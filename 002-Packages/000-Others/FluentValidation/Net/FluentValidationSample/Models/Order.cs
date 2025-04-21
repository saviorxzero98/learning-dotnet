namespace FluentValidationSample.Models
{
    public class Order
    {
        public string Id { get; set; }

        public Customer Customer { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();

        public DateTime OrderTime { get; set; }
    }
}
