using Org.OpenAPITools.Api;

namespace OpenApiGeneratorClientSample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await DemoAsync();
        }

        static async Task DemoAsync()
        {
            var httpClient = new UserApi("http://localhost:5081");

            // 取得使用者列表
            var users = await httpClient.UserGetListAsync("10", "0");


            // 上傳檔案
            using (var stream = File.OpenRead("avatar.png"))
            {
                var avatar = await httpClient.UserUploadAvatarAsync("abc", stream);
            }
        }
    }
}
