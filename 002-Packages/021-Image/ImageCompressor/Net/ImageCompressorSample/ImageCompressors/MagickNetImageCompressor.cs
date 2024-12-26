using ImageMagick;

namespace ImageCompressorSample.ImageCompressors
{
    public class MagickNetImageCompressor : ImageCompressorBase
    {
        public uint Quality { get; set; }

        public ImageFormatType Format { get; set; }

        public MagickNetImageCompressor()
        {
            Quality = 75;
        }
        public MagickNetImageCompressor(uint quality, ImageFormatType format = ImageFormatType.Jpeg)
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
            using (var image = new MagickImage(inputStream))
            using (var outputStream = new MemoryStream())
            {
                if (image == null)
                {
                    throw new InvalidOperationException("Failed to decode image.");
                }

                image.Format = GetFormat();
                image.Quality = Quality;

                image.Write(outputStream);
                return outputStream.ToArray();
            }
        }

        private MagickFormat GetFormat()
        {
            switch (Format)
            {
                case ImageFormatType.WebP:
                    return MagickFormat.WebP;
                case ImageFormatType.Png:
                    return MagickFormat.Png;
                case ImageFormatType.Bmp:
                    return MagickFormat.Bmp;
                case ImageFormatType.Gif:
                    return MagickFormat.Gif;
                case ImageFormatType.Ico:
                    return MagickFormat.Ico;
                case ImageFormatType.Jpeg:
                default:
                    return MagickFormat.Jpeg;
            }
        }
    }
}
