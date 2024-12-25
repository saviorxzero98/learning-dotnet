using ImageMagick;

namespace ImageCompressorSample.ImageCompressors
{
    public class MagickNetImageCompressor : ImageCompressorBase
    {
        public uint Quality { get; set; }

        public MagickNetImageCompressor()
        {
            Quality = 75;
        }
        public MagickNetImageCompressor(uint quality)
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
            using (var image = new MagickImage(inputStream))
            using (var outputStream = new MemoryStream())
            {
                if (image == null)
                {
                    throw new InvalidOperationException("Failed to decode image.");
                }

                image.Format = MagickFormat.Jpg;
                image.Quality = Quality;

                image.Write(outputStream);
                return outputStream.ToArray();
            }
        }
    }
}
