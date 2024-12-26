using SkiaSharp;

namespace ImageCompressorSample.ImageCompressors
{
    public class SkiaSharpImageCompressor : ImageCompressorBase
    {
        public int Quality { get; set; }

        public ImageFormatType Format { get; set; }

        public SkiaSharpImageCompressor()
        {
            Quality = 75;
        }
        public SkiaSharpImageCompressor(int quality, ImageFormatType format = ImageFormatType.Jpeg)
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
            using (var bitmap = SKBitmap.Decode(inputStream))
            using (var outputStream = new MemoryStream())
            {
                if (bitmap == null)
                {
                    throw new InvalidOperationException("Failed to decode image.");
                }

                var image = SKImage.FromBitmap(bitmap);
                var data = image.Encode(GetFormat(), Quality);
                data.SaveTo(outputStream);

                return outputStream.ToArray();
            }
        }

        private SKEncodedImageFormat GetFormat()
        {
            switch (Format) 
            {
                case ImageFormatType.WebP:
                    return SKEncodedImageFormat.Webp;
                case ImageFormatType.Png:
                    return SKEncodedImageFormat.Png;
                case ImageFormatType.Bmp:
                    return SKEncodedImageFormat.Bmp;
                case ImageFormatType.Gif:
                    return SKEncodedImageFormat.Gif;
                case ImageFormatType.Ico:
                    return SKEncodedImageFormat.Ico;
                case ImageFormatType.Jpeg:
                default:
                    return SKEncodedImageFormat.Jpeg;
            }
        }
    }
}
