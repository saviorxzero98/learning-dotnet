using System.Runtime.InteropServices;
using System.Security;

namespace EmailConfigurations.Extensions
{
    public static class SecureStringExtensions
    {
        /// <summary>
        /// 轉成一般字串
        /// </summary>
        /// <param name="secureString"></param>
        /// <returns></returns>
        public static string ToPlainString(this SecureString secureString)
        {
            if (secureString == null)
            {
                return string.Empty;
            }

            var ptr = Marshal.SecureStringToBSTR(secureString);

            try
            {
                return Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                Marshal.ZeroFreeBSTR(ptr);
            }
        }
    }
}
