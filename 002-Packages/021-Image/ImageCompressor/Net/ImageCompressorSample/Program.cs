using ImageCompressorSample.ImageCompressors;

namespace ImageCompressorSample
{
    public class Program
    {
        static void Main(string[] args)
        {
            var fileName = "Images\\Sample.png";

            var jpgExt = "jpg";
            var webpExt = "webp";

            CompressImage(fileName, "SystemDrawing", jpgExt, new SystemDrawingImageCompressor(80));
            CompressImage(fileName, "SkiaSharp (Jpg)", jpgExt, new SkiaSharpImageCompressor(80, ImageFormatType.Jpeg));
            CompressImage(fileName, "SkiaSharp (WebP)", webpExt, new SkiaSharpImageCompressor(80, ImageFormatType.WebP));
            CompressImage(fileName, "ImageSharp (Jpg)", jpgExt, new ImageSharpImageCompressor(80, ImageFormatType.Jpeg));
            CompressImage(fileName, "ImageSharp (WebP)", webpExt, new ImageSharpImageCompressor(80, ImageFormatType.WebP));
            CompressImage(fileName, "MagickNet (Jpg)", jpgExt, new MagickNetImageCompressor(80, ImageFormatType.Jpeg));
            CompressImage(fileName, "MagickNet (WebP)", webpExt, new MagickNetImageCompressor(80, ImageFormatType.WebP));
            CompressImage(fileName, "NetVips", jpgExt, new NetVipsImageCompressor());
        }


        static void CompressImage(string fileName, string name, string fileExt, IImageCompressor imageCompressor)
        {
            Console.WriteLine($"===== Demo {name} =====");

            Console.WriteLine("Read Image");
            var inputBytes = imageCompressor.ReadImage(fileName);

            Console.WriteLine("Compress Image");
            var outputBytes = imageCompressor.CompressImage(inputBytes);

            var outputFileName = $"Images\\{name}.{fileExt}";
            Console.WriteLine($"Save Image: {outputFileName}");
            imageCompressor.SaveImage(outputFileName, outputBytes);

            Console.WriteLine($"==========\n\n");
        }
    }
}
