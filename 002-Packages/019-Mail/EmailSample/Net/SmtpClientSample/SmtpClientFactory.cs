using EmailConfigurations;
using System.Net;
using System.Net.Mail;

namespace SmtpClientSample
{
    public static class SmtpClientFactory
    {
        public static SmtpClient Create(MailServerSettings setting)
        {
            if (setting == null)
            {
                throw new ArgumentNullException();
            }

            if (setting.UsePickupDirectory)
            {
                return CreateLocalDirectorySmtpClient(setting);
            }
            else
            {
                return CreateSmtpClient(setting);
            }
        }

        private static SmtpClient CreateSmtpClient(MailServerSettings setting)
        {
            var smtpClient = new SmtpClient(setting.HostUrl, setting.Port)
            {
                EnableSsl = setting.UseSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(setting.Account.UserName, setting.Account.Password)
            };
            return smtpClient;
        }


        private static SmtpClient CreateLocalDirectorySmtpClient(MailServerSettings setting)
        {
            var smtpClient = new SmtpClient(setting.HostUrl, setting.Port)
            {
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = setting.HostUrl
            };
            return smtpClient;
        }
    }
}
