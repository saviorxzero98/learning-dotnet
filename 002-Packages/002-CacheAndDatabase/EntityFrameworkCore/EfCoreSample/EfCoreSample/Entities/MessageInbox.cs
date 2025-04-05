namespace EfCoreSample.Entities
{
    public class MessageInbox
    {
        public Guid MessageId { get; set; }

        public Guid UserId { get; set; }

        public string Subject { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        public bool IsAlreadyRead { get; set; } = false;

        public DateTime? SendTime { get; set; }

        public DateTime? ReadTime { get; set; }
    }
}
