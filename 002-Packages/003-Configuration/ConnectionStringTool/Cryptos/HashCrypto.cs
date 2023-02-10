﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace ConnectionStringTool.Cryptos
{
    public class HashCrypto
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

        public HashCrypto(HashAlgorithmType algorithm)
        {
            Algorithm = algorithm;
        }

        /// <summary>
        ///  Hash Encode
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string Encode(string plainText)
        {
            if (plainText == null)
            {
                throw new ArgumentNullException(nameof(plainText));
            }

            // Hash
            HashAlgorithm algorithm = GetHashAlgorithm(Algorithm);
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            byte[] crypto = algorithm.ComputeHash(bytes);

            // To Base64
            return Base64.Encode(crypto);
        }

        /// <summary>
        /// Vaildate Hash
        /// </summary>
        /// <param name="vaildateText"></param>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public bool Vaildate(string vaildateText, string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                throw new ArgumentNullException(nameof(plainText));
            }
            if (string.IsNullOrEmpty(vaildateText))
            {
                throw new ArgumentNullException(nameof(vaildateText));
            }

            string cipherText = Encode(plainText);
            return cipherText.Equals(vaildateText);
        }

        /// <summary>
        /// Get Hash Algorithm
        /// </summary>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        protected HashAlgorithm GetHashAlgorithm(HashAlgorithmType algorithm)
        {
            switch (algorithm)
            {
                case HashAlgorithmType.SHA1:
                    return new SHA1CryptoServiceProvider();
                case HashAlgorithmType.SHA256:
                    return new SHA256CryptoServiceProvider();
                case HashAlgorithmType.SHA384:
                    return new SHA384CryptoServiceProvider();
                case HashAlgorithmType.SHA512:
                    return new SHA512CryptoServiceProvider();
                case HashAlgorithmType.MD5:
                default:
                    return MD5.Create();
            }
        }
    }
}
