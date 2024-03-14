using System.Security.Cryptography;
using System.Text;
using CryptoSample.Encoders;

namespace CryptoSample.Cryptos
{
    public class AsymmetricCrypto
    {
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public bool FOAEP { get; set; } = false;


        public EncryptedStringEncode StringEncode { get; set; } = EncryptedStringEncode.Base64;


        public AsymmetricCrypto()
        {
        }

        /// <summary>
        /// Generate Public Key And Private Key
        /// </summary>
        /// <returns></returns>
        public AsymmetricCryptoKey GenerateKey()
        {
            var provider = new RSACryptoServiceProvider();
            var publicKey = provider.ToXmlString(false);
            var privateKey = provider.ToXmlString(true);
            return new AsymmetricCryptoKey(publicKey, privateKey);
        }

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public string Encrypt(string plainText, string publicKey)
        {
            try
            {
                var provider = new RSACryptoServiceProvider();
                provider.FromXmlString(publicKey);
                
                var plain = Encoding.GetBytes(plainText);
                var bytes = provider.Encrypt(plain, FOAEP);

                return ToEncryptedString(bytes);
            }
            catch
            {
                return plainText;
            }
        }

        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="priveateKey"></param>
        /// <returns></returns>
        public string Decrypt(string cipherText, string priveateKey)
        {
            try
            {
                var provider = new RSACryptoServiceProvider();
                provider.FromXmlString(priveateKey);
                
                var cipher = FromEncryptedString(cipherText);
                var bytes = provider.Decrypt(cipher, FOAEP);

                return Encoding.GetString(bytes);
            }
            catch
            {
                return cipherText;
            }
        }


        #region Private

        /// <summary>
        /// Bytes轉字串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        protected string ToEncryptedString(byte[] bytes)
        {
            switch (StringEncode)
            {
                case EncryptedStringEncode.Hex:
                    return HexEncoder.Instance.ToHexString(bytes);

                case EncryptedStringEncode.Base64:
                    return Base64Encoder.Instance.ToBase64String(bytes);

                case EncryptedStringEncode.Raw:
                default:
                    return Encoding.GetString(bytes);
            }
        }

        /// <summary>
        /// 字串轉Bytes
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        protected byte[] FromEncryptedString(string cipherText)
        {
            switch (StringEncode)
            {
                case EncryptedStringEncode.Hex:
                    return HexEncoder.Instance.FromHexString(cipherText);

                case EncryptedStringEncode.Base64:
                    return Base64Encoder.Instance.FromBase64String(cipherText);

                case EncryptedStringEncode.Raw:
                default:
                    return Encoding.GetBytes(cipherText);
            }
        }

        #endregion
    }
}
