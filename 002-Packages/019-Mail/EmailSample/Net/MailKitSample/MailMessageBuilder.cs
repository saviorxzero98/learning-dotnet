using MimeKit;

namespace MailKitSample
{
    public class MailMessageBuilder
    {
        public MailboxAddress Sender { get; set; }

        public InternetAddressList From { get; set; } = new InternetAddressList();

        public InternetAddressList To { get; set; } = new InternetAddressList();

        public InternetAddressList Cc { get; set; } = new InternetAddressList();

        public InternetAddressList Bcc { get; set; } = new InternetAddressList();

        public string Subject { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;
        public bool IsBodyHtml { get; set; } = false;


        public MailMessageBuilder AddFrom(string address, string name)
        {
            From.Add(new MailboxAddress(name, address));
            return this;
        }
        public MailMessageBuilder AddTo(string address, string name)
        {
            To.Add(new MailboxAddress(name, address));
            return this;
        }
        public MailMessageBuilder AddCc(string address, string name)
        {
            Cc.Add(new MailboxAddress(name, address));
            return this;
        }
        public MailMessageBuilder AddBcc(string address, string name)
        {
            Bcc.Add(new MailboxAddress(name, address));
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



        public MimeMessage Build()
        {
            // 建立 Body
            var bodyBuilder = new BodyBuilder();
            if (IsBodyHtml)
            {
                bodyBuilder.HtmlBody = Body;
            }
            else
            {
                bodyBuilder.TextBody = Body;
            }

            // 建立 Mail Message
            var message = new MimeMessage()
            {
                Subject = Subject
            };
            message.From.AddRange(From);
            message.To.AddRange(To);
            message.Cc.AddRange(Cc);
            message.Bcc.AddRange(Bcc);
            message.Body = bodyBuilder.ToMessageBody();
            
            return message;
        }
    }
}
