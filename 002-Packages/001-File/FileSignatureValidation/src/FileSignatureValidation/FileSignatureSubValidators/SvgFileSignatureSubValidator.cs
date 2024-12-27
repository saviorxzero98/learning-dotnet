using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace FileSignatureValidation.FileSignatureSubValidators
{
    public class SvgFileSignatureSubValidator : IFileSignatureSubValidator
    {
        public const string RootNode = "svg";

        /// <summary>
        /// 符合的副檔名
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        protected bool IsMatch(string extension)
        {
            return extension == ".svg";
        }

        /// <summary>
        /// 驗證檔案與副檔名是否一致
        /// </summary>
        /// <param name="extension"></param>
        /// <param name="fileBytes"></param>
        /// <param name="allowedChars"></param>
        /// <returns>true: 符合; false: 不符合; null: 無法判斷</returns>
        public bool? IsValidFileExtension(string extension, byte[] fileBytes, byte[] allowedChars = null)
        {
            if (IsMatch(extension)) 
            {
                try
                {
                    using (var inputStream = new MemoryStream(fileBytes))
                    {
                        var xmlDoc = XDocument.Load(inputStream);

                        return xmlDoc.Root != null &&
                               RootNode.Equals(xmlDoc.Root.Name.LocalName, StringComparison.InvariantCultureIgnoreCase);
                    }
                }
                catch
                {
                    return false;
                }
            }
            return null;
        }
    }
}
