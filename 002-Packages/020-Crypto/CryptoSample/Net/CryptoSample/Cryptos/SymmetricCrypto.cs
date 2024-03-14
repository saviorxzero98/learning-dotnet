using System.Security.Cryptography;
using System.Text;
using CryptoSample.Encoders;

namespace CryptoSample.Cryptos
{
    public class SymmetricCrypto
    {
        public SymmetricAlgorithmType Algorithm { get; set; }
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        public CipherMode Cipher { get; set; } = CipherMode.CBC;
        public PaddingMode Padding { get; set; } = PaddingMode.PKCS7;
        public EncryptedStringEncode StringEncode { get; set; } = EncryptedStringEncode.Base64;

        public SymmetricCrypto()
        {
            Algorithm = SymmetricAlgorithmType.AES;
            Padding = PaddingMode.PKCS7;
            Cipher = CipherMode.CBC;
            Encoding = Encoding.UTF8;
            StringEncode = EncryptedStringEncode.Base64;
        }
        public SymmetricCrypto(SymmetricAlgorithmType algorithm)
        {
            Algorithm = algorithm;
            Padding = PaddingMode.PKCS7;
            Cipher = CipherMode.CBC;
            Encoding = Encoding.UTF8;
            StringEncode = EncryptedStringEncode.Base64;
        }


        /// <summary>
        /// 設定 Encoding
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public SymmetricCrypto SetEncoding(Encoding encoding)
        {
            Encoding = encoding;
            return this;
        }
        /// <summary>
        /// 設定 Cipher Mode
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns></returns>
        public SymmetricCrypto SetCipherMode(CipherMode cipher)
        {
            Cipher = cipher;
            return this;
        }
        /// <summary>
        /// 設定 Cipher Mode
        /// </summary>
        /// <param name="padding"></param>
        /// <returns></returns>
        public SymmetricCrypto SetPaddingMode(PaddingMode padding)
        {
            Padding = padding;
            return this;
        }


        /// <summary>
        /// Generate Key
        /// </summary>
        /// <returns></returns>
        public SymmetricCryptoKey GenerateKey()
        {
            SymmetricAlgorithm provider = CreateEncryptAlgorithm(Algorithm, Cipher, Padding);
            provider.GenerateKey();
            provider.GenerateIV();
            return new SymmetricCryptoKey(provider.Key, provider.IV);
        }


        #region Encrypt

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public string Encrypt(string plainText, byte[] key, byte[] iv)
        {
            return Encrypt(plainText, new SymmetricCryptoKey(key, iv));
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="base64Key"></param>
        /// <param name="base64Iv"></param>
        /// <returns></returns>
        public string Encrypt(string plainText, string base64Key, string base64Iv)
        {
            return Encrypt(plainText, SymmetricCryptoKey.FromBase64(base64Key, base64Iv));
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Encrypt(string plainText, SymmetricCryptoKey key)
        {
            try
            {
                SymmetricAlgorithm provider = CreateEncryptAlgorithm(Algorithm, Cipher, Padding);
                provider.Key = key.Key;
                provider.IV = key.IV;
                byte[] plain = Encoding.GetBytes(plainText);

                ICryptoTransform desencrypt = provider.CreateEncryptor();
                byte[] bytes = desencrypt.TransformFinalBlock(plain, 0, plain.Length);

                return ToEncryptedString(bytes);
            }
            catch
            {
                return plainText;
            }
        }

        #endregion


        #region Decrypt

        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public string Decrypt(string cipherText, byte[] key, byte[] iv)
        {
            return Decrypt(cipherText, new SymmetricCryptoKey(key, iv));
        }
        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="base64Key"></param>
        /// <param name="base64Iv"></param>
        /// <returns></returns>
        public string Decrypt(string cipherText, string base64Key, string base64Iv)
        {
            return Decrypt(cipherText, SymmetricCryptoKey.FromBase64(base64Key, base64Iv));
        }
        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Decrypt(string cipherText, SymmetricCryptoKey key)
        {
            try
            {
                SymmetricAlgorithm provider = CreateEncryptAlgorithm(Algorithm, Cipher, Padding);
                provider.Key = key.Key;
                provider.IV = key.IV;

                byte[] cipher = FromEncryptedString(cipherText);

                ICryptoTransform desencrypt = provider.CreateDecryptor();
                byte[] bytes = desencrypt.TransformFinalBlock(cipher, 0, cipher.Length);

                return Encoding.GetString(bytes);
            }
            catch
            {
                return cipherText;
            }
        }

        #endregion


        #region Private

        /// <summary>
        /// Create Crypto Algorithm
        /// </summary>
        /// <param name="algorithm"></param>
        /// <param name="mode"></param>
        /// <param name="padding"></param>
        /// <returns></returns>
        protected SymmetricAlgorithm CreateEncryptAlgorithm(SymmetricAlgorithmType algorithm, CipherMode mode, PaddingMode padding)
        {
            switch (algorithm)
            {
                case SymmetricAlgorithmType.Rijndael:
                    return new RijndaelManaged()
                    {
                        Mode = mode,
                        Padding = padding
                    };

                case SymmetricAlgorithmType.AES:
                default:
                    return new AesCryptoServiceProvider()
                    {
                        Mode = mode,
                        Padding = padding
                    };
            }
        }

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
