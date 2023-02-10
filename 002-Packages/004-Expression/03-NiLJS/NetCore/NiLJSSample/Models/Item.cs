using System.Collections.Generic;

namespace NiLJSSample.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string[] Items { get; set; }

        public List<Item> SubItem { get; set; } = new List<Item>();
    }
}
