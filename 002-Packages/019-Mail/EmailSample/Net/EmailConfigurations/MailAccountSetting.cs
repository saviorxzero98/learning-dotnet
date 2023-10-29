using EmailConfigurations.Extensions;
using System.Security;

namespace EmailConfigurations
{
    public class MailAccountSetting
    {
        public string UserName { get; set; }

        private SecureString _password;
        public string Password
        {
            get
            {
                return _password.ToPlainString();
            }
            set
            {
                _password = value.ToSecureString();
            }
        }

        public MailAccountSetting()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }
        public MailAccountSetting(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
