using System;
using System.Security.Cryptography;
using System.Text;

namespace ConnectionStringTool.Cryptos
{
    public class SymmetricCrypto
    {
        public SymmetricAlgorithmType Algorithm { get; set; }
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        public CipherMode Mode { get; set; } = CipherMode.CBC;
        public PaddingMode Padding { get; set; } = PaddingMode.PKCS7;

        public SymmetricCrypto()
        {
            Algorithm = SymmetricAlgorithmType.DES;
            Padding = PaddingMode.PKCS7;
            Mode = CipherMode.CBC;
            Encoding = Encoding.UTF8;
        }
        public SymmetricCrypto(SymmetricAlgorithmType algorithm)
        {
            Algorithm = algorithm;
            Padding = PaddingMode.PKCS7;
            Mode = CipherMode.CBC;
            Encoding = Encoding.UTF8;
        }

        /// <summary>
        /// Generate Key
        /// </summary>
        /// <returns></returns>
        public SymmetricCryptoKey GenerateKey()
        {
            SymmetricAlgorithm provider = CreateEncryptAlgorithm(Algorithm, Mode, Padding);
            provider.GenerateKey();
            provider.GenerateIV();
            return new SymmetricCryptoKey(provider.Key, provider.IV);
        }

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
            return Encrypt(plainText, new SymmetricCryptoKey(base64Key, base64Iv));
        }
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public string Encrypt(string plainText, SymmetricCryptoKey key)
        {
            try
            {
                SymmetricAlgorithm provider = CreateEncryptAlgorithm(Algorithm, Mode, Padding);
                provider.Key = key.Key;
                provider.IV = key.IV;
                byte[] plain = Encoding.GetBytes(plainText);

                ICryptoTransform desencrypt = provider.CreateEncryptor();
                byte[] bytes = desencrypt.TransformFinalBlock(plain, 0, plain.Length);

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
        /// <param name="keyBase64"></param>
        /// <param name="ivBase64"></param>
        /// <returns></returns>
        public string Decrypt(string cipherText, string base64Key, string base64Iv)
        {
            return Decrypt(cipherText, new SymmetricCryptoKey(base64Key, base64Iv));
        }
        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public string Decrypt(string cipherText, SymmetricCryptoKey key)
        {
            try
            {
                SymmetricAlgorithm provider = CreateEncryptAlgorithm(Algorithm, Mode, Padding);
                provider.Key = key.Key;
                provider.IV = key.IV;
                byte[] cipher = Convert.FromBase64String(cipherText);

                ICryptoTransform desencrypt = provider.CreateDecryptor();
                byte[] bytes = desencrypt.TransformFinalBlock(cipher, 0, cipher.Length);

                return Encoding.GetString(bytes);
            }
            catch
            {
                return cipherText;
            }
        }

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
                case SymmetricAlgorithmType.AES:
                    return new AesCryptoServiceProvider()
                    {
                        Mode = mode,
                        Padding = padding
                    };
                case SymmetricAlgorithmType.TripleDES:
                    return new TripleDESCryptoServiceProvider()
                    {
                        Mode = mode,
                        Padding = padding
                    };
                case SymmetricAlgorithmType.RC2:
                    return new RC2CryptoServiceProvider()
                    {
                        Mode = mode,
                        Padding = padding
                    };
                case SymmetricAlgorithmType.Rijndael:
                    return new RijndaelManaged()
                    {
                        Mode = mode,
                        Padding = padding
                    };
                case SymmetricAlgorithmType.DES:
                default:
                    return new DESCryptoServiceProvider()
                    {
                        Mode = mode,
                        Padding = padding
                    };
            }
        }
    }

    public class SymmetricCryptoKey
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }

        public SymmetricCryptoKey()
        {

        }
        public SymmetricCryptoKey(byte[] key, byte[] iv)
        {
            Key = key;
            IV = iv;
        }
        public SymmetricCryptoKey(string base64Key, string base64Iv)
        {
            Key = Convert.FromBase64String(base64Key);
            IV = Convert.FromBase64String(base64Iv);
        }

        public string Base64Key
        {
            get
            {
                return Convert.ToBase64String(Key);
            }
        }

        public string Base64IV
        {
            get
            {
                return Convert.ToBase64String(IV);
            }
        }
    }
}
