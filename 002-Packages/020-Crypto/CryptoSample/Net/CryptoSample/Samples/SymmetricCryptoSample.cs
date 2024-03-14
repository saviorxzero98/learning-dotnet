using CryptoSample.Cryptos;

namespace CryptoSample.Samples
{
    public class SymmetricCryptoSample
    {
        public static void ExecuteSample()
        {
            Console.WriteLine("==================== Hash Encoder ====================");
            (new SymmetricCryptoSample()).Run();
            Console.WriteLine("=========================================================");
        }

        private void Run()
        {
            string plainText = "Hi 你好";

            Console.WriteLine($"DES\nPlain Text: {plainText}\n");

            RunCrypto(SymmetricAlgorithmType.AES, plainText);
            RunCrypto(SymmetricAlgorithmType.Rijndael, plainText);
        }

        public void RunCrypto(SymmetricAlgorithmType algorithm, string plainText)
        {
            SymmetricCrypto crypto = new SymmetricCrypto(algorithm);

            // Generate Key And IV
            SymmetricCryptoKey key = crypto.GenerateKey();

            Console.WriteLine($"[{algorithm.ToString()}] (Key: {key.Key}; IV: {key.IV})");

            // Encrypt
            DateTime encryptStart = DateTime.Now;
            string cipherText = crypto.Encrypt(plainText, key);

            DateTime encryptEnd = DateTime.Now;
            double encryptTimeSpan = (encryptEnd - encryptStart).TotalMilliseconds;

            Console.WriteLine($"Encrypt : {cipherText} \n({encryptTimeSpan} ms)\n");

            DateTime decryptStart = DateTime.Now;

            // Decrypt
            string orgText = crypto.Decrypt(cipherText, key);

            DateTime decryptEnd = DateTime.Now;
            double decrypteTimeSpan = (decryptEnd - decryptStart).TotalMilliseconds;

            Console.WriteLine($"Decrypt : {orgText} \n({decrypteTimeSpan} ms)\n\n");
        }
    }
}
