namespace MediatorPatternSample
{
    public class Message
    {
        public string Subject { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;


        public string RecipientName { get; set; } = string.Empty;

        public string RecipientEmail { get; set; } = string.Empty;

        public string FromName { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;


        public Message(string subject, string content = "")
        {
            Subject = subject;
            Content = content;
        }
        public Message(Message message)
        {
            Subject = message.Subject;
            Content = message.Content;
        }


        public Message From(string name, string email = "")
        {
            FromName = name;
            FromEmail = email;
            return this;
        }

        public Message To(string name, string email = "")
        {
            RecipientName = name;
            RecipientEmail = email;
            return this;
        }
    }
}
