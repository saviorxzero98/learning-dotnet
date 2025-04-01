namespace FusionCacheSample.Models
{
    public class Book
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string Publisher { get; set; } = string.Empty;

        public List<string> Categories { get; set; } = new List<string>();
    }
}
