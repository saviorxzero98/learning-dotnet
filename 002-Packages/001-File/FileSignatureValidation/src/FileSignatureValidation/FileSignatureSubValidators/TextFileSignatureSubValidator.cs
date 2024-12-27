using System.Linq;

namespace FileSignatureValidation.FileSignatureSubValidators
{
    public class TextFileSignatureSubValidator : IFileSignatureSubValidator
    {
        /// <summary>
        /// 符合的副檔名
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        protected bool IsMatch(string extension)
        {
            return extension.Equals(".txt") || extension.Equals(".csv") || extension.Equals(".prn");
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
                foreach (byte b in fileBytes)
                {
                    if (b > 0xFF)
                    {
                        if (allowedChars != null)
                        {
                            if (!allowedChars.Contains(b))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return null;
        }
    }
}
