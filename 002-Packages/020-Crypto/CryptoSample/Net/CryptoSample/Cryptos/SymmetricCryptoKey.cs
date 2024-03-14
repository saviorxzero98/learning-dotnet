using System.Text;

namespace CryptoSample.Cryptos
{
    public class SymmetricCryptoKey
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }

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

        public SymmetricCryptoKey()
        {

        }
        public SymmetricCryptoKey(byte[] key, byte[] iv)
        {
            Key = key;
            IV = iv;
        }


        public static SymmetricCryptoKey FromBase64(string key, string iv)
        {
            return new SymmetricCryptoKey()
            {
                Key = Convert.FromBase64String(key),
                IV = Convert.FromBase64String(iv)
            };
        }
        public static SymmetricCryptoKey FromText(string key, string iv, Encoding encoding)
        {
            return new SymmetricCryptoKey()
            {
                Key = encoding.GetBytes(key),
                IV = encoding.GetBytes(iv)
            };
        }
    }
}
