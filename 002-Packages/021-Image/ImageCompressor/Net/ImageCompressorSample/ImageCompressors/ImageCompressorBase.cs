namespace ImageCompressorSample.ImageCompressors
{
    public abstract class ImageCompressorBase : IImageCompressor
    {
        /// <summary>
        /// 讀取圖片
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public virtual byte[] ReadImage(string fileName)
        {
            if (!File.Exists(fileName)) 
            {
                throw new FileNotFoundException();
            }

            return File.ReadAllBytes(fileName);
        }

        /// <summary>
        /// 儲存圖片
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileBytes"></param>
        public bool SaveImage(string fileName, byte[] fileBytes)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(fileBytes, 0, fileBytes.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }

        /// <summary>
        /// 壓縮圖片
        /// </summary>
        /// <param name="inputBytes"></param>
        /// <returns></returns>
        public abstract byte[] CompressImage(byte[] inputBytes);
    }
}
