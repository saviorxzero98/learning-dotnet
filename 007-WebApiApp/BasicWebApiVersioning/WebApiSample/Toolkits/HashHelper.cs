using System;
using System.Security.Cryptography;
using System.Text;

namespace WebApiSample.Toolkits
{
    public static class HashHelper
    {
        public static string Hash(string plainText)
        {
            // Hash
            HashAlgorithm algorithm = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            byte[] crypto = algorithm.ComputeHash(bytes);

            // To Base64
            var result = Convert.ToBase64String(crypto);
            return result.Replace("=", "")
                         .Replace("/", "_")
                         .Replace("+", "-");
        }
    }
}
