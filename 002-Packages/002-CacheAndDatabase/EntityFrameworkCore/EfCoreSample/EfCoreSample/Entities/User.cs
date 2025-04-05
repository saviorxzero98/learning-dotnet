using UUIDNext;

namespace EfCoreSample.Entities
{
    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsActived { get; set; } = true;

        public User()
        {
            Id = Uuid.NewDatabaseFriendly(Database.SqlServer).ToString();
        }
        public User(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
