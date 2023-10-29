using EmailConfigurations;
using MailKit.Net.Smtp;
using MimeKit;

namespace MailKitSample
{
    public class MailSenderClient : IDisposable
    {
        protected MailServerSettings Setting { get; set; }

        protected SmtpClient _smtpClient;


        public MailSenderClient(MailServerSettings setting) 
        {
            Setting = setting;
        }

        public void Dispose()
        {
            if (_smtpClient != null)
            {
                if (_smtpClient.IsConnected)
                {
                    _smtpClient.Disconnect(true);
                }

                _smtpClient.Dispose();
            }            
        }


        public void SendMail(MimeMessage message)
        {
            if (Setting.UsePickupDirectory)
            {
                SaveToPickupDirectory(message);
            }
            else
            {
                Send(message);
            }
        }
        public async Task SendMailAsync(MimeMessage message)
        {
            if (Setting.UsePickupDirectory)
            {
                await SaveToPickupDirectoryAsync(message);
            }
            else
            {
                await SendAsync(message);
            }
        }


        #region File (Pickup Directory)

        public void SaveToPickupDirectory(MimeMessage message)
        {
            var fileName = $"{Guid.NewGuid()}.eml";
            var filePath = PathHelper.CombinePath(PathHelper.ToAbsolutePath(Setting.HostUrl),
                                                  fileName);                
            message.WriteTo(filePath);
        }

        public Task SaveToPickupDirectoryAsync(MimeMessage message)
        {
            return message.WriteToAsync(Setting.HostUrl);
        }

        #endregion


        #region SMTP

        public void Send(MimeMessage message)
        {
            _smtpClient = new SmtpClient();
            _smtpClient.Connect(Setting.HostUrl, Setting.Port, Setting.UseSsl);
            _smtpClient.Authenticate(Setting.Account.UserName, Setting.Account.Password);
            _smtpClient.Send(message);
        }

        public async Task SendAsync(MimeMessage message)
        {
            _smtpClient = new SmtpClient();
            await _smtpClient.ConnectAsync(Setting.HostUrl, Setting.Port, Setting.UseSsl);
            await _smtpClient.AuthenticateAsync(Setting.Account.UserName, Setting.Account.Password);
            await _smtpClient.SendAsync(message);
        }

        #endregion
    }
}
