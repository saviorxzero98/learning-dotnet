using Newtonsoft.Json.Linq;
using StringCompressSample.TextCompressors;
using System.Text;

namespace StringCompressSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var text = "Hello, World!";
            var data = ReadJson();


            DemoText(text, TextCompressor.Deflate, "Deflate");
            DemoText(text, TextCompressor.GZip, "GZip");
            DemoText(text, TextCompressor.ZLib, "ZLib");
            DemoText(text, TextCompressor.Brotli, "Brotli");
            DemoText(text, new ZstdNetCompressor(), "Zstandard (Net)");
            DemoText(text, new ZstdSharpCompressor(), "Zstandard (Sharp)");
            DemoText(text, new LZ4Compressor(), "LZ4");
            DemoText(text, new SnappierCompressor(), "Snappier");

            DemoObject(data, TextCompressor.Deflate, "Deflate");
            DemoObject(data, TextCompressor.GZip, "GZip");
            DemoObject(data, TextCompressor.ZLib, "ZLib");
            DemoObject(data, TextCompressor.Brotli, "Brotli");
            DemoObject(data, new ZstdNetCompressor(), "Zstandard (Net)");
            DemoObject(data, new ZstdSharpCompressor(), "Zstandard (Sharp)");
            DemoObject(data, new LZ4Compressor(), "LZ4");
            DemoObject(data, new SnappierCompressor(), "Snappier");
        }

        static void DemoText(string text, ICompressor compressor, string type)
        {
            double fileSize = Encoding.UTF8.GetBytes(text).Length;

            var compressStartTime = DateTime.Now;
            var compressBytes = compressor.CompressText(text);
            var compressEndTime = DateTime.Now;

            double compressSize = compressBytes.Length;
            double compressPercentage = (compressSize / fileSize) * 100;

            Console.WriteLine($"===== 文字壓縮 ({type}) =====");
            Console.WriteLine("[壓縮]");
            Console.WriteLine($"內容: {Convert.ToBase64String(compressBytes)}");
            Console.WriteLine($"大小: {compressSize} bytes");
            Console.WriteLine($"時間: {(compressEndTime - compressStartTime).TotalMilliseconds} ms");
            Console.WriteLine($"壓縮率: {string.Format("{0:0.00}", compressPercentage)}%");

            Console.WriteLine("");

            var decompressStartTime = DateTime.Now;
            var decompressText = compressor.DecompressText(compressBytes);
            var decompressEndTime = DateTime.Now;

            Console.WriteLine("[解壓縮]");
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
            double compressPercentage = (compressSize / fileSize) * 100;

            Console.WriteLine($"===== JSON 字串解壓縮 ({type}) =====");
            Console.WriteLine("[壓縮]");
            //Console.WriteLine($"內容: {Convert.ToBase64String(compressBytes)}");
            Console.WriteLine($"大小: {compressSize} bytes");
            Console.WriteLine($"時間: {(compressEndTime - compressStartTime).TotalMilliseconds} ms");
            Console.WriteLine($"壓縮率: {string.Format("{0:0.00}", compressPercentage)}%");

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