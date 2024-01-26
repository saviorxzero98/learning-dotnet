using KiotaDemoApiClient;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace KiotaApiClientSample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await DemoAsync();
        }

        static async Task DemoAsync()
        {
            var authProvider = new AnonymousAuthenticationProvider();
            var adapter = new HttpClientRequestAdapter(authProvider);
            var httpClient = new DemoApiClient(adapter);

            // 取得使用者列表
            var results = await httpClient.Api.User.GetAsync((options) =>
            {
                options.QueryParameters.Limit = "10";
                options.QueryParameters.Offset = "0";
            });


            // 上傳檔案
            using (var stream = File.OpenRead("avatar.png"))
            {
                var body = new MultipartBody();
                body.AddOrReplacePart("avatar.png", "application/octet-stream", stream);

                var avatar = await httpClient.Api.User["abc"].Avatar.PostAsync(body);
            }

            
        }
    }
}
