namespace CryptoSample.Cryptos
{
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
