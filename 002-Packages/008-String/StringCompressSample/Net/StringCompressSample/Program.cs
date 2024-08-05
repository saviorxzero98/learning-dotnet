using Newtonsoft.Json.Linq;
using StringCompressSample.TextCompressors;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace StringCompressSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var text = "Hello, World!";
            var data = ReadJson();


            DemoText(text, new DeflateCompressor(), "Deflate");
            DemoText(text, new GZipCompressor(), "GZip");
            DemoText(text, new BrotliCompressor(), "Brotli");

            DemoObject(data, new DeflateCompressor(), "Deflate");
            DemoObject(data, new GZipCompressor(), "GZip");
            DemoObject(data, new BrotliCompressor(), "Brotli");
        }

        static void DemoText(string text, ICompressor compressor, string type)
        {
            double fileSize = Encoding.UTF8.GetBytes(text).Length;

            var compressStartTime = DateTime.Now;
            var compressBytes = compressor.CompressText(text);
            var compressEndTime = DateTime.Now;

            double compressSize = compressBytes.Length;

            Console.WriteLine($"===== 文字壓縮 ({type}) =====");
            Console.WriteLine($"內容: {Convert.ToBase64String(compressBytes)}");
            Console.WriteLine($"大小: {compressSize} bytes");
            Console.WriteLine($"時間: {(compressEndTime - compressStartTime).TotalMilliseconds} ms");
            Console.WriteLine($"壓縮率: {(compressSize / fileSize) * 100}%");

            Console.WriteLine("");

            var decompressStartTime = DateTime.Now;
            var decompressText = compressor.DecompressText(compressBytes);
            var decompressEndTime = DateTime.Now;

            Console.WriteLine($"===== 文字解壓縮 ({type}) =====");
            Console.WriteLine($"內容: {decompressText}");
            Console.WriteLine($"大小: {fileSize} bytes");
            Console.WriteLine($"時間: {(decompressEndTime - decompressStartTime).TotalMilliseconds} ms");

            Console.WriteLine("");
        }
    
        static void DemoObject(JObject data, ICompressor compressor, string type)
        {
            double fileSize = Encoding.UTF8.GetBytes(data.ToString()).Length;

            var compressStartTime = DateTime.Now;
            var compressBytes = compressor.CompressObject(data);
            var compressEndTime = DateTime.Now;

            double compressSize = compressBytes.Length;

            Console.WriteLine(type);
            Console.WriteLine("[壓縮]");
            Console.WriteLine($"內容: {Convert.ToBase64String(compressBytes)}");
            Console.WriteLine($"大小: {compressSize} bytes");
            Console.WriteLine($"時間: {(compressEndTime - compressStartTime).TotalMilliseconds} ms");
            Console.WriteLine($"壓縮率: {(compressSize / fileSize) * 100}%");

            Console.WriteLine("");

            var decompressStartTime = DateTime.Now;
            var decompressData = compressor.DecompressObject<JObject>(compressBytes);
            var decompressEndTime = DateTime.Now;

            Console.WriteLine("[解壓縮]");
            //Console.WriteLine($"內容: {gzipDecompressData.ToString()}");
            Console.WriteLine($"大小: {fileSize} bytes");
            Console.WriteLine($"時間: {(decompressEndTime - decompressStartTime).TotalMilliseconds} ms");

            Console.WriteLine("");
        }

        static JObject ReadJson()
        {
            var jsonString = File.ReadAllText("Data/Card.json");

            return JObject.Parse(jsonString);
        }
    }
}