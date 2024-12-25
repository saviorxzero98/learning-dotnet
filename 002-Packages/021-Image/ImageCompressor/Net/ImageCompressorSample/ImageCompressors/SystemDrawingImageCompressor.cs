using System.Drawing;
using System.Drawing.Imaging;

namespace ImageCompressorSample.ImageCompressors
{
    public class SystemDrawingImageCompressor : ImageCompressorBase
    {
        public long Quality { get; set; }

        public SystemDrawingImageCompressor()
        {
            Quality = 75L;
        }
        public SystemDrawingImageCompressor(long quality)
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
            using (var image = Image.FromStream(inputStream))
            using (var outputStream = new MemoryStream())
            {
                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, Quality);

                var jpegCodec = ImageCodecInfo.GetImageDecoders()
                                              .FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);

                if (jpegCodec == null)
                {
                    throw new InvalidOperationException("JPEG encoder not found.");
                }

                image.Save(outputStream, jpegCodec, encoderParameters);
                return outputStream.ToArray();
            }
        }
    }
}
