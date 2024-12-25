using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace ImageCompressorSample.ImageCompressors
{
    public class ImageSharpImageCompressor : ImageCompressorBase
    {
        public int Quality { get; set; }

        public ImageSharpImageCompressor()
        {
            Quality = 75;
        }
        public ImageSharpImageCompressor(int quality)
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
            using (var image = Image.Load(inputStream))
            using (var outputStream = new MemoryStream())
            {
                if (image == null)
                {
                    throw new InvalidOperationException("Failed to decode image.");
                }

                var encoder = new JpegEncoder { Quality = Quality };
                image.Save(outputStream, encoder);

                return outputStream.ToArray();
            }
        }
    }
}
