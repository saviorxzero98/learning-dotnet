using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using HeyRed.Mime;
using System.Net.Mime;

namespace BlobServiceSample.Services
{
    public class BlobUploadService
    {
        public const string DefaultContainerName = "dev-storage";

        public string ConnectionString { get; protected set; }

        public string ContainerName { get; protected set; }

        public double ExpireDays { get; set; } = 1;


        public BlobUploadService(string connectionString)
        {
            ConnectionString = connectionString;
            ContainerName = DefaultContainerName;
        }

        /// <summary>
        /// 上傳檔案
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="fileName"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string> UploadAsync(Stream fileStream, string fileName, CancellationToken token = default)
        {
            CheckUploadArguments(fileStream, fileName);

            var client = new BlobServiceClient(ConnectionString);

            // 取得 Blob Container
            var containerName = ContainerName;
            var container = client.GetBlobContainerClient(containerName);
            await container.CreateIfNotExistsAsync().ConfigureAwait(false);


            // 上傳 Blob
            var blobName = GetBlobName(fileName);
            var blob = container.GetBlobClient(blobName);
            var uploadOptions = new BlobUploadOptions()
            {
                HttpHeaders = new BlobHttpHeaders()
                {
                    ContentType = MimeTypesMap.GetMimeType(fileName)
                }
            };
            await blob.UploadAsync(fileStream, uploadOptions).ConfigureAwait(false);

            // 設定 Shared Access Signature (SAS)
            var expireDays = (ExpireDays > 0) ? ExpireDays : 1;
            var sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = containerName,
                BlobName = blobName,
                Resource = "b",
                ExpiresOn = DateTimeOffset.UtcNow.AddDays(expireDays),
                ContentDisposition = new ContentDisposition("attachment") { FileName = fileName }.ToString()
            };
            sasBuilder.SetPermissions(BlobSasPermissions.Read);
            var downloadUri = blob.GenerateSasUri(sasBuilder);

            return downloadUri.ToString();
        }

        /// <summary>
        /// 變更 Container Name
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public BlobUploadService ChangeContainerName(string containerName)
        {
            ContainerName = containerName;
            return this;
        }

        /// <summary>
        /// 設定檔案有效時間
        /// </summary>
        /// <param name="expireDays"></param>
        /// <returns></returns>
        public BlobUploadService SetExpireDays(double expireDays)
        {
            ExpireDays = expireDays;
            return this;
        }

        /// <summary>
        /// 檢查上傳的參數
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="fileName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        protected void CheckUploadArguments(Stream fileStream, string fileName)
        {
            if (fileStream == null)
            {
                throw new ArgumentNullException(nameof(fileStream));
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (string.IsNullOrWhiteSpace(ConnectionString))
            {
                throw new ArgumentNullException(nameof(ConnectionString));
            }

            if (string.IsNullOrWhiteSpace(ContainerName))
            {
                throw new ArgumentNullException(nameof(ContainerName));
            }
        }

        /// <summary>
        /// 取得隨機的 Blob Name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected virtual string GetBlobName(string fileName)
        {
            return $"{Guid.NewGuid()}-{fileName}";
        }
    }
}
