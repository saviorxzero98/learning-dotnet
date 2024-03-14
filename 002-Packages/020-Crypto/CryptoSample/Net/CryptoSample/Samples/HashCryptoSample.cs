using CryptoSample.Cryptos;

namespace CryptoSample.Samples
{
    public class HashCryptoSample
    {
        public static void ExecuteSample()
        {
            Console.WriteLine("==================== Hash Encoder ====================");
            (new HashCryptoSample()).Run();
            Console.WriteLine("=========================================================");
        }

        private void Run()
        {
            string testText = "Hi. 你好";

            Console.WriteLine($"Plain Text: {testText}\n\n");

            RunEncode(HashAlgorithmType.MD5, testText);
            RunEncode(HashAlgorithmType.SHA1, testText);
            RunEncode(HashAlgorithmType.SHA256, testText);
            RunEncode(HashAlgorithmType.SHA384, testText);
            RunEncode(HashAlgorithmType.SHA512, testText);
        }

        public void RunEncode(HashAlgorithmType algorithm, string plainText)
        {
            DateTime start = DateTime.Now;

            HashCrypto crypto = new HashCrypto(algorithm);
            string cipherText = crypto.Encode(plainText);

            DateTime end = DateTime.Now;
            double timeSpan = (end - start).TotalMilliseconds;

            Console.WriteLine($"{algorithm.ToString()} : {cipherText} \n({timeSpan} ms)\n\n");
        }
    }
}
