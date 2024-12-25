using ImageCompressorSample.ImageCompressors;

namespace ImageCompressorSample
{
    public class Program
    {
        static void Main(string[] args)
        {
            var fileName = "Images\\Sample.png";

            CompressImage(fileName, "SystemDrawing", new SystemDrawingImageCompressor());
            CompressImage(fileName, "SkiaSharp", new SkiaSharpImageCompressor());
            CompressImage(fileName, "ImageSharp", new ImageSharpImageCompressor());
            CompressImage(fileName, "MagickNet", new MagickNetImageCompressor());
            CompressImage(fileName, "NetVips", new NetVipsImageCompressor());
        }


        static void CompressImage(string fileName, string name, IImageCompressor imageCompressor)
        {
            Console.WriteLine($"===== Demo {name} =====");

            Console.WriteLine("Read Image");
            var inputBytes = imageCompressor.ReadImage(fileName);

            Console.WriteLine("Compress Image");
            var outputBytes = imageCompressor.CompressImage(inputBytes);

            var outputFileName = $"Images\\{name}.jpg";
            Console.WriteLine($"Save Image: {outputFileName}");
            imageCompressor.SaveImage(outputFileName, outputBytes);

            Console.WriteLine($"==========\n\n");
        }
    }
}
