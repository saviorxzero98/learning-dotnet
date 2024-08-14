using Newtonsoft.Json;
using System.IO.Compression;

namespace StringCompressSample.TextCompressors
{
    public enum TextCompressorType
    {
        Deflate,
        GZip,
        Brotli,
        ZLib
    }

    public class TextCompressor : ICompressor
    {
        public TextCompressorType Type { get; set; }

        public TextCompressor() : this(TextCompressorType.Brotli)
        {

        }
        public TextCompressor(TextCompressorType type)
        {
            Type = type;
        }

        public static TextCompressor Deflate { get => new TextCompressor(TextCompressorType.Deflate); }
        public static TextCompressor GZip { get => new TextCompressor(TextCompressorType.GZip); }
        public static TextCompressor Brotli { get => new TextCompressor(TextCompressorType.Brotli); }
        public static TextCompressor ZLib { get => new TextCompressor(TextCompressorType.ZLib); }


        /// <summary>
        /// 壓縮文字
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public byte[] CompressText(string text)
        {
            using (var memoryStream = new MemoryStream())
            using (var compressionStream = CreateCompressionStream(memoryStream, CompressionMode.Compress))
            using (var streamWriter = new StreamWriter(compressionStream))
            {
                streamWriter.Write(text);
                streamWriter.Close();
                compressionStream.Close();

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
            using (var compressionStream = CreateCompressionStream(memoryStream, CompressionMode.Decompress))
            using (var streamReader = new StreamReader(compressionStream))
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

        /// <summary>
        /// 建立 Compression Stream
        /// </summary>
        /// <param name="inStream"></param>
        /// <param name="mode"></param>
        /// <param name="leaveOpen"></param>
        /// <returns></returns>
        protected Stream CreateCompressionStream(Stream inStream, CompressionMode mode, bool leaveOpen = false)
        {
            switch (Type)
            {
                case TextCompressorType.Deflate:
                    return new DeflateStream(inStream, mode, leaveOpen);

                case TextCompressorType.GZip:
                    return new GZipStream(inStream, mode, leaveOpen);

                case TextCompressorType.ZLib:
                    return new ZLibStream(inStream, mode, leaveOpen);

                case TextCompressorType.Brotli:
                default:
                    return new BrotliStream(inStream, mode, leaveOpen);
            }
        }
    }
}
