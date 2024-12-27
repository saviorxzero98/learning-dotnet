namespace FileSignatureValidation.FileSignatureSubValidators
{
    public interface IFileSignatureSubValidator
    {
        /// <summary>
        /// 驗證檔案與副檔名是否一致
        /// </summary>
        /// <param name="extension"></param>
        /// <param name="fileBytes"></param>
        /// <param name="allowedChars"></param>
        /// <returns>true: 符合; false: 不符合; null: 無法判斷</returns>
        bool? IsValidFileExtension(string extension, byte[] fileBytes, byte[] allowedChars = null);
    }
}
