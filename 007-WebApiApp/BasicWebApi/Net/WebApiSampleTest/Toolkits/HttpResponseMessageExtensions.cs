using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text;
using static WebApiSampleTest.Toolkits.HttpApiClient;

namespace WebApiSampleTest.Toolkits
{
    public static class HttpResponseMessageEx
    {
        /// <summary>
        /// Get Response Text Content 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string GetTextContent(this HttpResponseMessage response)
        {
            List<string> contentEncoding = response.Content.Headers.ContentEncoding.ToList();

            // 檢查是否為壓縮格式
            bool isCompression = contentEncoding.Any();
            if (isCompression)
            {
                // 解壓縮訊息
                Stream stream = response.GetStreamContent();
                Stream compressionStream = GetCompressionStream(stream, contentEncoding);
                Encoding encoding = response.GetContentEncoding();

                if (compressionStream != null)
                {
                    using (StreamReader reader = new StreamReader(compressionStream, encoding))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }

            return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }


        /// <summary>
        /// Get Compression Stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="contentEncoding"></param>
        /// <returns></returns>
        private static Stream GetCompressionStream(Stream stream, List<string> contentEncoding)
        {
            if (contentEncoding.Any(e => e.ToLower() == ContentEncoding.GZip))
            {
                return new GZipStream(stream, CompressionMode.Decompress);
            }
            else if (contentEncoding.Any(e => e.ToLower() == ContentEncoding.Deflate))
            {
                return new DeflateStream(stream, CompressionMode.Decompress);
            }
            else if (contentEncoding.Any(e => e.ToLower() == ContentEncoding.Brotli))
            {
                return new BrotliStream(stream, CompressionMode.Decompress);
            }
            return null;
        }


        /// <summary>
        /// Get Response Stream Content 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static Stream GetStreamContent(this HttpResponseMessage response)
        {
            return response.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
        }


        /// <summary>
        /// Get Response Content (JToken)
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static JToken GetJsonContentModel(this HttpResponseMessage response)
        {
            // Get Encoding & Content Type
            Encoding encoding = response.GetContentEncoding();
            string contentType = response.GetContentType();

            // 取出 Response Content Text
            string contentText = response.GetTextContent();

            if (response.IsSuccessStatusCode)
            {
                // 依據 Content Type 轉換成 Model
                switch (contentType.ToLower())
                {
                    case ContentType.Json:
                        return JToken.Parse(contentText);

                    case ContentType.Xml:
                        return XmlConverter.DeserializeObject(contentText, encoding);

                    default:
                        return new JValue(contentText);
                }
            }
            return default(JToken);
        }
        /// <summary>
        /// Try Get Response Json Content
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="contentModel"></param>
        /// <returns></returns>
        public static bool TryGetJsonContentModel(this HttpResponseMessage response, out JToken contentModel)
        {
            try
            {
                contentModel = GetJsonContentModel(response);
                return true;
            }
            catch
            {
                contentModel = default(JToken);
                return false;
            }
        }


        /// <summary>
        /// Get Response Content Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public static T GetContentModel<T>(this HttpResponseMessage response)
        {
            // Get Encoding & Content Type
            Encoding encoding = response.GetContentEncoding();
            string contentType = response.GetContentType();

            // 取出 Response Content Text
            string contentText = response.GetTextContent();

            // 依據 Content Type 轉換成 Model
            switch (contentType.ToLower())
            {
                case ContentType.Json:
                    return JsonConvert.DeserializeObject<T>(contentText);
                case ContentType.Xml:
                    return XmlConverter.DeserializeObject<T>(contentText, encoding);
            }

            return default(T);
        }
        /// <summary>
        /// Try Get Response Content Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="contentModel"></param>
        /// <returns></returns>
        public static bool TryGetContentModel<T>(this HttpResponseMessage response, out T contentModel)
        {
            try
            {
                contentModel = GetContentModel<T>(response);
                return (contentModel != null);
            }
            catch
            {
                contentModel = default(T);
                return false;
            }
        }


        /// <summary>
        /// 取得 Content Encoding
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static Encoding GetContentEncoding(this HttpResponseMessage response)
        {
            string contentCharSet = response.GetContentCharSet();

            if (!string.IsNullOrEmpty(contentCharSet))
            {
                try
                {
                    Encoding encoding = Encoding.GetEncoding(contentCharSet);
                    return encoding;
                }
                catch
                {

                }
            }
            return Encoding.Default;
        }

        /// <summary>
        /// 取得 Content Chat Set
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string GetContentCharSet(this HttpResponseMessage response)
        {
            bool hasCharSet = (response.Content != null &&
                               response.Content.Headers != null &&
                               response.Content.Headers.ContentType != null &&
                               !string.IsNullOrEmpty(response.Content.Headers.ContentType.CharSet));

            if (hasCharSet)
            {
                return response.Content.Headers.ContentType.CharSet;
            }
            return string.Empty;
        }

        /// <summary>
        /// 取得 Content Type
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string GetContentType(this HttpResponseMessage response)
        {

            bool hasMediaType = (response.Content != null &&
                                 response.Content.Headers != null &&
                                 response.Content.Headers.ContentType != null &&
                                 !string.IsNullOrEmpty(response.Content.Headers.ContentType.MediaType));

            if (hasMediaType)
            {
                return response.Content.Headers.ContentType.MediaType;
            }
            return string.Empty;
        }
    }
}
