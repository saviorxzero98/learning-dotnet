namespace StringCompressSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var text = "Hello, World!";

            var compressor = new TextCompressor();
            var compressText = compressor.Compress(text);
            Console.WriteLine(compressText);

            var decompressText = compressor.Decompress(compressText);
            Console.WriteLine(decompressText);
        }
    }
}