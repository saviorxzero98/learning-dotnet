using CryptoSample.Cryptos;

namespace CryptoSample.Samples
{
    public class AsymmetricCryptoSample
    {
        public static void ExecuteSample()
        {
            Console.WriteLine("==================== Hash Encoder ====================");
            (new AsymmetricCryptoSample()).Run();
            Console.WriteLine("=========================================================");
        }

        private void Run()
        {
            string plainText = "Hi";

            Console.WriteLine($"RSA\nPlain Text: {plainText}\n");

            AsymmetricCrypto crypto = new AsymmetricCrypto();

            // Generate Key And IV
            AsymmetricCryptoKey key = crypto.GenerateKey();

            Console.WriteLine($"Public Key:\n{key.PublicKey}");
            Console.WriteLine($"Private Key:\n{key.PrivateKey}");

            // Encrypt
            DateTime encryptStart = DateTime.Now;
            string cipherText = crypto.Encrypt(plainText, key.PublicKey);

            DateTime encryptEnd = DateTime.Now;
            double encryptTimeSpan = (encryptEnd - encryptStart).TotalMilliseconds;

            Console.WriteLine($"Encrypt : {cipherText} \n({encryptTimeSpan} ms)\n");

            DateTime decryptStart = DateTime.Now;

            // Decrypt
            string orgText = crypto.Decrypt(cipherText, key.PrivateKey);

            DateTime decryptEnd = DateTime.Now;
            double decrypteTimeSpan = (decryptEnd - decryptStart).TotalMilliseconds;

            Console.WriteLine($"Decrypt : {orgText} \n({decrypteTimeSpan} ms)\n\n");
        }
    }
}
