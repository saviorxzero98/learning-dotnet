namespace ImageCompressorSample.ImageCompressors
{
    public interface IImageCompressor
    {
        /// <summary>
        /// 讀取圖片
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        byte[] ReadImage(string filePath);

        /// <summary>
        /// 儲存圖片
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileBytes"></param>
        bool SaveImage(string fileName, byte[] fileBytes);

        /// <summary>
        /// 圖片轉換壓縮
        /// </summary>
        /// <param name="inputBytes"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        byte[] CompressImage(byte[] inputBytes);
    }
}
