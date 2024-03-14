using CryptoSample.Cryptos;

namespace CryptoSample.Samples
{
    public class HmacHashCryptoSample
    {
        public static void ExecuteSample()
        {
            Console.WriteLine("==================== Hash Encoder ====================");
            (new HmacHashCryptoSample()).Run();
            Console.WriteLine("=========================================================");
        }

        private void Run()
        {
            string testText = "Hi. 你好";
            string secretText = "25867890";

            Console.WriteLine($"Plain Text: {testText}\nSecret Text: {secretText}\n\n");

            RunEncode(HashAlgorithmType.MD5, testText, secretText);
            RunEncode(HashAlgorithmType.SHA1, testText, secretText);
            RunEncode(HashAlgorithmType.SHA256, testText, secretText);
            RunEncode(HashAlgorithmType.SHA384, testText, secretText);
            RunEncode(HashAlgorithmType.SHA512, testText, secretText);
        }

        public void RunEncode(HashAlgorithmType algorithm, string plainText, string secretText)
        {
            DateTime start = DateTime.Now;

            HmacHashCrypto crypto = new HmacHashCrypto(algorithm);
            string cipherText = crypto.Encode(plainText, secretText);

            DateTime end = DateTime.Now;
            double timeSpan = (end - start).TotalMilliseconds;

            Console.WriteLine($"{algorithm.ToString()} : {cipherText} \n({timeSpan} ms)\n\n");
        }
    }
}
