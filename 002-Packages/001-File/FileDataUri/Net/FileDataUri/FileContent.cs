namespace FileDataUri
{
    public class FileContent
    {
        /// <summary>
        /// 檔案內容
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// MIME
        /// </summary>
        public string MimeType {  get; set; } = string.Empty;
    }
}
