namespace YamlParserSample.Models
{
    public class OrganizationUnit
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public List<OrganizationUnit> Children { get; set; } = new List<OrganizationUnit>();
    }
}
