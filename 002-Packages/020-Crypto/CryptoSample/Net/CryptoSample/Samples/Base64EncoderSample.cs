using CryptoSample.Encoders;

namespace CryptoSample.Samples
{
    public class Base64EncoderSample
    {
        public static void ExecuteSample()
        {
            Console.WriteLine("==================== Base 64 Encoder ====================");
            (new Base64EncoderSample()).Run();
            Console.WriteLine("=========================================================");
        }

        private void Run()
        {
            string testText = "Hi. 你好";
            string encodeText = Base64Encoder.Instance.Encode(testText);
            string decodeText = Base64Encoder.Instance.Decode(encodeText);


            Console.WriteLine($"Text: {testText}");
            Console.WriteLine($"Encode: {encodeText}");
            Console.WriteLine($"Decode: {decodeText}");
        }
    }
}
