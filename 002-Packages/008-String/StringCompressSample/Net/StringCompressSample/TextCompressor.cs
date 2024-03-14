using Newtonsoft.Json;
using System.IO.Compression;

namespace StringCompressSample
{
    public class TextCompressor
    {
        /// <summary>
        /// 壓縮字串
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Compress(string text)
        {
            using (var memoryStream = new MemoryStream())
            using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
            using (var streamWriter = new StreamWriter(gzipStream))
            {
                streamWriter.Write(text);
                streamWriter.Close();
                gzipStream.Close();

                var bytes = memoryStream.ToArray();
                return Convert.ToBase64String(bytes);
            }
        }
        /// <summary>
        /// 壓縮物件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Compress<T>(T data) where T : class
        {
            if (data == null)
            {
                return string.Empty;
            }

            var settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore
            };
            string serializedJson = JsonConvert.SerializeObject(data, settings);
            return Compress(serializedJson);
        }


        /// <summary>
        /// 解壓縮
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Decompress(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty; 
            }

            byte[] bytes = Convert.FromBase64String(text);

            using (var memoryStream = new MemoryStream(bytes))
            using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
            using (var streamReader = new StreamReader(gzipStream))
            {
                return streamReader.ReadToEnd();
            }
        }

        /// <summary>
        /// 解壓縮
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public T Decompress<T>(string text) where T : class
        {
            if (string.IsNullOrEmpty(text))
            {
                return default(T);
            }

            var deserializeJson = Decompress(text);
            return JsonConvert.DeserializeObject<T>(deserializeJson);
        }
    }
}
