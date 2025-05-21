using BlobServiceSample.Services;
using Microsoft.Extensions.Configuration;

namespace BlobServiceSample.Samples
{
    public class BlobUploadSample : DemoSampleBase
    {
        public BlobUploadSample(IConfiguration configuration) : base(configuration)
        {
        }

        protected override async Task HandleAsync()
        {
            var connectionString = Configuration.GetValue("AzureBlobStorage:ConnectionString", string.Empty);
            var containerName = Configuration.GetValue("AzureBlobStorage:ContainerName", "dev-storage");
            var expireDays = Configuration.GetValue("AzureBlobStorage:ExpireDays", 1);

            try
            {
                Console.WriteLine($"正在讀取檔案...\n");

                // 讀檔案
                string fileName = "Data.json";
                string filePath = "Files";
                var stream = ReadFile(Path.Combine(filePath, fileName));

                Console.WriteLine("讀取檔案成功\n");
                Console.WriteLine("正在上傳檔案...\n");

                // 上傳檔案
                var service = new BlobUploadService(connectionString!)
                                    .ChangeContainerName(containerName!)
                                    .SetExpireDays(expireDays);
                var url = await service.UploadAsync(stream, fileName);

                Console.WriteLine("檔案上傳成功\n");
                Console.WriteLine($"檔案下載連結：\n{url}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        protected Stream ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }
                
            byte[] fileBytes = File.ReadAllBytes(filePath);
            MemoryStream memoryStream = new MemoryStream(fileBytes);

            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
