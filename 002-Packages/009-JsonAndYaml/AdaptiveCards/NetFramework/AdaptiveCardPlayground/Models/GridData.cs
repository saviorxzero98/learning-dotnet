using System.Collections.Generic;

namespace AdaptiveCardPlayground.Models
{
    public class GridData
    {
        public List<GridItem> Cards { get; set; } = new List<GridItem>();
    }

    public class GridItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }

        public GridItem()
        {

        }
        public GridItem(int id, string name, string style)
        {
            Id = id;
            Name = name;
            Style = style;
        }
    }
}
