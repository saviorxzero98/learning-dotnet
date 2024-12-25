using NetVips;

namespace ImageCompressorSample.ImageCompressors
{
    public class NetVipsImageCompressor : ImageCompressorBase
    {
        /// <summary>
        /// 圖片壓縮
        /// </summary>
        /// <param name="inputBytes"></param>
        /// <returns></returns>
        public override byte[] CompressImage(byte[] inputBytes)
        {
            using (var inputStream = new MemoryStream(inputBytes))
            using (var image = Image.NewFromStream(inputStream))
            using (var outputStream = new MemoryStream())
            {
                if (image == null)
                {
                    throw new InvalidOperationException("Failed to decode image.");
                }

                image.WriteToStream(outputStream, ".jpg");
                return outputStream.ToArray();
            }
        }
    }
}
