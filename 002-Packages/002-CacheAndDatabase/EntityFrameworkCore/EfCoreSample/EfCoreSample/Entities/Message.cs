using UUIDNext;

namespace EfCoreSample.Entities
{
    public class Message
    {
        public Guid Id { get; set; }

        public string Subject { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        public DateTime? SendTime { get; set; }

        public Message()
        {
            Id = Uuid.NewDatabaseFriendly(Database.SqlServer);
        }
    }
}
