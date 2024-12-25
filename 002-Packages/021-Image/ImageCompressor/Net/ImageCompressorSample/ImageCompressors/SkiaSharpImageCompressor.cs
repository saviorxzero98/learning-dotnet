using SkiaSharp;

namespace ImageCompressorSample.ImageCompressors
{
    public class SkiaSharpImageCompressor : ImageCompressorBase
    {
        public int Quality { get; set; }

        public SkiaSharpImageCompressor()
        {
            Quality = 75;
        }
        public SkiaSharpImageCompressor(int quality)
        {
            Quality = quality;
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
                var data = image.Encode(SKEncodedImageFormat.Jpeg, Quality);
                data.SaveTo(outputStream);

                return outputStream.ToArray();
            }
        }
    }
}
