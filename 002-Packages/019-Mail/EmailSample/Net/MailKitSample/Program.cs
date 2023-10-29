using EmailConfigurations;
using Microsoft.Extensions.Configuration;

namespace MailKitSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var setting = MailServerSettings.Read(ReadConfigutation());

            DemoSendMail(setting);
        }

        static void DemoSendMail(MailServerSettings setting)
        {
            // 建立 Mail 內容
            var builder = new MailMessageBuilder()
                .AddFrom("user_a@test.org", "User A")
                .AddTo("user_b@test.org", "User B")
                .AddTo("user_c@test.org", "User C")
                .SetSubject("This is test mail")
                .SetBody("<html><h1>Hello, my friend</h1></html>", true);
            var mailMessage = builder.Build();

            // 發送 Mail
            using (var client = new MailSenderClient(setting))
            {
                client.SendMail(mailMessage);

                Console.WriteLine("Send mail success.");
            }
        }


        static IConfiguration ReadConfigutation()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json",
                                                                           optional: true,
                                                                           reloadOnChange: true)
                                                              .AddJsonFile("appsettings.Development.json",
                                                                           optional: true,
                                                                           reloadOnChange: true)
                                                              .Build();
            return config;
        }
    }
}