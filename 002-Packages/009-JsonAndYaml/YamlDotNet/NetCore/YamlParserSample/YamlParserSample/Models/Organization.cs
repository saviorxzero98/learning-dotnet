namespace YamlParserSample.Models
{
    public class Organization
    {
        public List<OrganizationUnit> OrganizationUnits { get; set; } = new List<OrganizationUnit>();

        public List<User> Users { get; set; } = new List<User>();

        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
