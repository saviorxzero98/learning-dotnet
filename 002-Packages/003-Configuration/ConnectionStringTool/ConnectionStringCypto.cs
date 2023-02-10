using ConnectionStringTool.Cryptos;

namespace ConnectionStringTool
{
    internal static class ConnectionStringCypto
    {
        private const string ASEPrivateKey = "private_key";

        private const string ASEPublicKey = "public_key";

        internal static string Encrypt(string plainText)
        {
            var crypto = new SymmetricCrypto(SymmetricAlgorithmType.AES);
            string key = GetHash(ASEPrivateKey);
            string lv = GetHash(ASEPublicKey);
            return crypto.Encrypt(plainText, key, lv);
        }

        internal static string Decrypt(string secretText)
        {
            var crypto = new SymmetricCrypto(SymmetricAlgorithmType.AES);
            string key = GetHash(ASEPrivateKey);
            string lv = GetHash(ASEPublicKey);
            return crypto.Decrypt(secretText, key, lv);
        }

        internal static string GetHash(string plainText)
        {
            var crypto = new HashCrypto(HashAlgorithmType.MD5);

            return crypto.Encode(plainText);
        }
    }
}
