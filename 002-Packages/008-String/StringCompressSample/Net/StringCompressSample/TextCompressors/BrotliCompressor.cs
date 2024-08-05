using Newtonsoft.Json;
using System.IO.Compression;

namespace StringCompressSample.TextCompressors
{
    public class BrotliCompressor : ICompressor
    {
        /// <summary>
        /// 壓縮文字
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public byte[] CompressText(string text)
        {
            using (var memoryStream = new MemoryStream())
            using (var brotliStream = new BrotliStream(memoryStream, CompressionMode.Compress))
            using (var streamWriter = new StreamWriter(brotliStream))
            {
                streamWriter.Write(text);
                streamWriter.Close();
                brotliStream.Close();

                var bytes = memoryStream.ToArray();
                return bytes;
            }
        }
        /// <summary>
        /// 解壓縮文字
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string DecompressText(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return string.Empty;
            }

            using (var memoryStream = new MemoryStream(bytes))
            using (var brotliStream = new BrotliStream(memoryStream, CompressionMode.Decompress))
            using (var streamReader = new StreamReader(brotliStream))
            {
                return streamReader.ReadToEnd();
            }
        }


        /// <summary>
        /// 壓縮物件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] CompressObject<T>(T data) where T : class
        {
            if (data == null)
            {
                return Array.Empty<byte>();
            }

            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore
            };
            string serializedJson = JsonConvert.SerializeObject(data, settings);
            return CompressText(serializedJson);
        }
        /// <summary>
        /// 解壓縮物件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public T DecompressObject<T>(byte[] bytes) where T : class
        {
            if (bytes == null || bytes.Length == 0)
            {
                return default(T);
            }

            var deserializeJson = DecompressText(bytes);

            if (string.IsNullOrEmpty(deserializeJson))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(deserializeJson);
        }
    }
}
