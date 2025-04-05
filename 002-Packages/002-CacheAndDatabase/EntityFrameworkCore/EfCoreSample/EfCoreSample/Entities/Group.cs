using System.ComponentModel.DataAnnotations.Schema;
using UUIDNext;

namespace EfCoreSample.Entities
{
    public class Group
    {
        public string Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [NotMapped]
        public List<User> Members { get; set; } = new List<User>();


        public Group()
        {
            Id = Uuid.NewDatabaseFriendly(Database.SqlServer).ToString();
        }
        public Group(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
