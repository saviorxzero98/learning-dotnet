using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Webp;

namespace ImageCompressorSample.ImageCompressors
{
    public class ImageSharpImageCompressor : ImageCompressorBase
    {
        public int Quality { get; set; }

        public ImageFormatType Format { get; set; }

        public ImageSharpImageCompressor()
        {
            Quality = 75;
        }
        public ImageSharpImageCompressor(int quality, ImageFormatType format = ImageFormatType.Jpeg)
        {
            Quality = quality;
            Format = format;
        }

        /// <summary>
        /// 圖片壓縮
        /// </summary>
        /// <param name="inputBytes"></param>
        /// <returns></returns>
        public override byte[] CompressImage(byte[] inputBytes)
        {
            using (var inputStream = new MemoryStream(inputBytes))
            using (var image = Image.Load(inputStream))
            using (var outputStream = new MemoryStream())
            {
                if (image == null)
                {
                    throw new InvalidOperationException("Failed to decode image.");
                }

                var encoder = GetImageEncoder();
                image.Save(outputStream, encoder);

                return outputStream.ToArray();
            }
        }


        private ImageEncoder GetImageEncoder()
        {
            switch (Format)
            {
                case ImageFormatType.WebP:
                    return new WebpEncoder() { Quality = Quality };
                case ImageFormatType.Png:
                    return new PngEncoder();
                case ImageFormatType.Bmp:
                    return new BmpEncoder();
                case ImageFormatType.Gif:
                    return new GifEncoder();
                case ImageFormatType.Jpeg:
                default:
                    return new JpegEncoder() { Quality = Quality }; ;
            }
        }
    }
}
