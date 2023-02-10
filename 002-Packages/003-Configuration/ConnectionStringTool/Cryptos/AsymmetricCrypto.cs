using System;
using System.Security.Cryptography;
using System.Text;

namespace ConnectionStringTool.Cryptos
{
    public class AsymmetricCrypto
    {
        public Encoding Encoding { get; set; } = Encoding.UTF8;


        public AsymmetricCrypto()
        {
            Encoding = Encoding.UTF8;
        }

        /// <summary>
        /// Generate Public Key And Private Key
        /// </summary>
        /// <returns></returns>
        public AsymmetricCryptoKey GenerateKey()
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            string publicKey = provider.ToXmlString(false);
            string privateKey = provider.ToXmlString(true);
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
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(publicKey);
                byte[] plain = Encoding.GetBytes(plainText);

                byte[] bytes = provider.Encrypt(plain, false);

                return Convert.ToBase64String(bytes);
            }
            catch
            {
                return plainText;
            }
        }

        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="desencryptText"></param>
        /// <param name="priveateKey"></param>
        /// <returns></returns>
        public string Decrypt(string cipherText, string priveateKey)
        {
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(priveateKey);
                byte[] cipher = Convert.FromBase64String(cipherText);

                byte[] bytes = provider.Decrypt(cipher, false);

                return Encoding.GetString(bytes);
            }
            catch
            {
                return cipherText;
            }
        }
    }

    public class AsymmetricCryptoKey
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }

        public AsymmetricCryptoKey()
        {

        }
        public AsymmetricCryptoKey(string publicKey, string privateKey)
        {
            PublicKey = publicKey;
            PrivateKey = privateKey;
        }
    }
}
