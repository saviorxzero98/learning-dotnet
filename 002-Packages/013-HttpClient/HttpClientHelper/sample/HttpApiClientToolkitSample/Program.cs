using HttpApiClientToolkit;
using HttpApiClientToolkit.Extensions;

namespace HttpApiClientToolkitSample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            DemoHttpGetCookie();

            await DemoDownloadFileAsync();
        }

        static void DemoHttpGetCookie()
        {
            var client = new HttpApiClient();


            var response = client.Get("https://www.google.com.tw");

            var cookies = response.GetSetCookies();
        }

        /// <summary>
        /// Demo Download File
        /// </summary>
        static async Task DemoDownloadFileAsync()
        {
            string url = "https://www.google.com.tw/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png";
            string fileName = "GoogleLogo.png";

            var client = new HttpApiClient();

            var response = client.GetAsync(url)
                                 .GetAwaiter()
                                 .GetResult();

            if (response.IsSuccessStatusCode)
            {
                var downloadStream = await response.Content.ReadAsStreamAsync();

                var fileStream = File.Create(fileName);
                downloadStream.Seek(0, SeekOrigin.Begin);
                downloadStream.CopyTo(fileStream);
                fileStream.Close();
            }
        }
    }
}
