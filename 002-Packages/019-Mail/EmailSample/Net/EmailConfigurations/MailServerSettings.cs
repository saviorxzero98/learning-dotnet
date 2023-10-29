using Microsoft.Extensions.Configuration;

namespace EmailConfigurations
{
    public class MailServerSettings
    {
        public const string SettingName = "EmailServer";

        /// <summary>
        /// 是否儲存 EML 檔案 (本機)
        /// </summary>
        public bool UsePickupDirectory { get; set; } = false;

        /// <summary>
        /// Host Url
        /// </summary>
        /// <remarks>
        /// UsePickupDirectory = true 時，這裡請設定資料夾路徑。否則指定 Host URL
        /// </remarks>
        public string HostUrl { get; set; } = string.Empty;

        /// <summary>
        /// Port
        /// </summary>
        public int Port { get; set; } = 25;

        /// <summary>
        /// 是否使用 SSL
        /// </summary>
        public bool UseSsl { get; set; } = false;

        /// <summary>
        /// Mail Server 帳密
        /// </summary>
        public MailAccountSetting Account { get; set; } = new MailAccountSetting();


        public static MailServerSettings Read(IConfiguration configuration)
        {
            if (configuration != null)
            {
                var settings = configuration.GetSection(SettingName).Get<MailServerSettings>();
                return settings;
            }
            return default(MailServerSettings);
        }
    }
}
