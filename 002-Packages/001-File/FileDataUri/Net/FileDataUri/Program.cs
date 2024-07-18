using MimeMapping;
using System.Text.RegularExpressions;

namespace FileDataUri
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string folder = "Files";
            var files = GetFiles(folder);

            // 將檔案轉換成 Data URI
            var fileDataUriList = new List<string>();
            foreach (var file in files)
            {
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                using (var stream = new MemoryStream())
                {
                    fs.CopyTo(stream);
                    var mimeType = MimeUtility.GetMimeMapping(file);
                    fileDataUriList.Add(ToDataUri(stream.ToArray(), mimeType));
                }
            }

            // 將 Data URI 轉成 File Binary
            var fileContentList = new List<FileContent>();
            foreach (var fileDataUri in fileDataUriList)
            {
                var fileContent = ToFileContent(fileDataUri);
                fileContentList.Add(fileContent);

                Console.WriteLine(fileContent.MimeType);
            }
        }

        /// <summary>
        /// Bytes 轉 Data URI
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        static string ToDataUri(byte[] bytes, string mimeType)
        {
            string base64String = Convert.ToBase64String(bytes);
            string dataUri = $"data:{mimeType};base64,{base64String}";
            return dataUri;
        }

        /// <summary>
        /// Data URI 轉 Bytes
        /// </summary>
        /// <param name="dataUri"></param>
        /// <returns></returns>
        static FileContent ToFileContent(string dataUri)
        {
            var matchGroups = Regex.Match(dataUri, @"^data:((?<type>[\w\.\+\/]+))?;base64,(?<data>.+)$").Groups;
            var base64String = matchGroups["data"].Value;
            var mimeType = matchGroups["type"].Value;
            var bytes = Convert.FromBase64String(base64String);

            return new FileContent()
            {
                Content = bytes,
                MimeType = mimeType
            };
        }

        /// <summary>
        /// 取得目錄下的檔案
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        static List<string> GetFiles(string folderPath)
        {
            return Directory.GetFiles(folderPath).ToList();
        }
    }
}
