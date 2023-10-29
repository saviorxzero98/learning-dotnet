using System.Net.Mail;
using System.Text;

namespace SmtpClientSample
{
    public class MailMessageBuilder
    {
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public string Sender { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public string Recipient { get; set; } = string.Empty;
        public string RecipientName { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public bool IsBodyHtml { get; set; } = false;


        public MailMessageBuilder SetSender(string address, string name)
        {
            Sender = address;
            SenderName = name;
            return this;
        }

        public MailMessageBuilder SetRecipient(string address, string name)
        {
            Recipient = address;
            RecipientName = name;
            return this;
        }

        public MailMessageBuilder SetSubject(string subject) 
        {
            Subject = subject;
            return this;
        }

        public MailMessageBuilder SetBody(string body, bool isHtmlBody = false)
        {
            Body = body;
            IsBodyHtml = isHtmlBody;
            return this;
        }

        public MailMessage Build()
        {
            var from = new MailAddress(Sender, SenderName, Encoding);
            var to = new MailAddress(Recipient, RecipientName, Encoding);

            var message = new MailMessage(from, to)
            {
                Subject = Subject,
                SubjectEncoding = Encoding,
                Body = Body,
                BodyEncoding = Encoding,
                IsBodyHtml = IsBodyHtml,
                Priority = MailPriority.Normal
            };

            return message;
        }
    }
}
