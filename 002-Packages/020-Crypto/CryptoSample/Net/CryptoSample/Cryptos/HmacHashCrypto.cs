using System.Security.Cryptography;
using System.Text;
using CryptoSample.Encoders;

namespace CryptoSample.Cryptos
{
    public class HmacHashCrypto
    {
        public HashAlgorithmType Algorithm { get; set; } = HashAlgorithmType.SHA256;
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        public bool UrlEncodeFlag { get; set; } = false;

        protected Base64Encoder Base64
        {
            get
            {
                return new Base64Encoder()
                {
                    UrlEncodeFlag = UrlEncodeFlag,
                    Encoding = Encoding
                };
            }
        }

        public HmacHashCrypto(HashAlgorithmType algorithm)
        {
            Algorithm = algorithm;
        }

        /// <summary>
        /// Hash Encode
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="secretText"></param>
        /// <returns></returns>
        public string Encode(string plainText, string secretText)
        {
            if (plainText == null)
            {
                throw new ArgumentNullException(nameof(plainText));
            }
            if (secretText == null)
            {
                throw new ArgumentNullException(nameof(secretText));
            }

            HashAlgorithm algorithm = GetHashAlgorithm(Algorithm, secretText);
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            byte[] crypto = algorithm.ComputeHash(bytes);
            return Base64.ToBase64String(crypto);
        }

        /// <summary>
        /// Vaildate Hash
        /// </summary>
        /// <param name="type"></param>
        /// <param name="vaildateText"></param>
        /// <param name="plainText"></param>
        /// <param name="secretText"></param>
        /// <returns></returns>
        public bool Vaildate(string vaildateText, string plainText, string secretText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                throw new ArgumentNullException(nameof(plainText));
            }
            if (string.IsNullOrEmpty(secretText))
            {
                throw new ArgumentNullException(nameof(secretText));
            }
            if (string.IsNullOrEmpty(vaildateText))
            {
                throw new ArgumentNullException(nameof(vaildateText));
            }

            string cipherText = Encode(plainText, secretText);
            return cipherText.Equals(vaildateText);
        }

        /// <summary>
        /// Get Hash Algorithm
        /// </summary>
        /// <param name="algorithm"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        protected HashAlgorithm GetHashAlgorithm(HashAlgorithmType algorithm, string secret)
        {
            switch (algorithm)
            {
                case HashAlgorithmType.SHA1:
                    return new HMACSHA1(Encoding.UTF8.GetBytes(secret));
                case HashAlgorithmType.SHA256:
                    return new HMACSHA256(Encoding.UTF8.GetBytes(secret));
                case HashAlgorithmType.SHA384:
                    return new HMACSHA384(Encoding.UTF8.GetBytes(secret));
                case HashAlgorithmType.SHA512:
                    return new HMACSHA512(Encoding.UTF8.GetBytes(secret));
                case HashAlgorithmType.MD5:
                default:
                    return new HMACMD5(Encoding.UTF8.GetBytes(secret));
            }
        }
    }
}
