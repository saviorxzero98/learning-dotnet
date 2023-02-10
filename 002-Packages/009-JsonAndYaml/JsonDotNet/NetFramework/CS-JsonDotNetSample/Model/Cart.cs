using System.Collections.Generic;

namespace CS_JsonDotNetSample.Model
{
    public class Cart
    {
        public List<Product> Products { get; private set; } = new List<Product>();

        public string Name { get; set; }
    }
}
