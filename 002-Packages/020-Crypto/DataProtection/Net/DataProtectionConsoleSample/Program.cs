using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace DataProtectionConsoleSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = GetServiceProvider();
            var provider = services.GetDataProtectionProvider();


            var dataProtector = provider.CreateProtector("Sample");

            var text = "Hello World";
            var secretText = dataProtector.Protect(text);
            var plainText = dataProtector.Unprotect(secretText);

            Console.WriteLine($"Text: {text}");
            Console.WriteLine($"Secret Text: {secretText}");
            Console.WriteLine($"Plain Text: {plainText}");
        }


        static IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddDataProtection()
                    .PersistKeysToFileSystem(new DirectoryInfo("Keys"));
            return services.BuildServiceProvider();
        }
    }
}
