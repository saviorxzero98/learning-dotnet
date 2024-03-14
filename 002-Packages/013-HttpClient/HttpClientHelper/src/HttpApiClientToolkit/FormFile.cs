using System.IO;

namespace HttpApiClientToolkit
{
    /// <summary>
    /// Form File Content
    /// </summary>
    public class FormFile
    {
        /// <summary>
        /// Form Key
        /// </summary>
        public string FormKey { get; set; }

        /// <summary>
        /// File Name
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// File Stream
        /// </summary>
        public Stream FileStream { get; set; }

        public FormFile()
        {

        }
        public FormFile(string formKey, Stream fileStream, string fileName = "")
        {
            FormKey = formKey;
            FileName = fileName;
            FileStream = fileStream;
        }
    }
}
