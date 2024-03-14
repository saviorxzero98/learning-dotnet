using CryptoSample.Encoders;

namespace CryptoSample.Samples
{
    public class HexEncoderSample
    {
        public static void ExecuteSample()
        {
            Console.WriteLine("==================== Base 64 Encoder ====================");
            (new HexEncoderSample()).Run();
            Console.WriteLine("=========================================================");
        }

        private void Run()
        {
            string testText = "Hi. 你好";
            string encodeText = HexEncoder.Instance.Encode(testText);
            string decodeText = HexEncoder.Instance.Decode(encodeText);


            Console.WriteLine($"Text: {testText}");
            Console.WriteLine($"Encode: {encodeText}");
            Console.WriteLine($"Decode: {decodeText}");
        }
    }
}
