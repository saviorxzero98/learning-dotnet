namespace StringCompressSample.TextCompressors
{
    public interface ICompressor
    {
        /// <summary>
        /// 壓縮文字
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        byte[] CompressText(string text);

        /// <summary>
        /// 解壓縮文字
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        string DecompressText(byte[] bytes);


        /// <summary>
        /// 壓縮物件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        byte[] CompressObject<T>(T data) where T : class;

        /// <summary>
        /// 解壓縮物件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        T DecompressObject<T>(byte[] bytes) where T : class;
    }
}
