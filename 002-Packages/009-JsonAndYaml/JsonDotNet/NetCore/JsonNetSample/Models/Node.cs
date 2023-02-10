using System.Collections.Generic;

namespace JsonNetSample.Models
{
    public class Node
    {
        public string Id { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public List<Node> Children { get; set; } = new List<Node>();
    }
}
