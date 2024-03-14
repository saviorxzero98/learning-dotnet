using CryptoSample.Encoders;

namespace CryptoSample.Samples
{
    public class UnicodeEncoderSample
    {
        public static void ExecuteSample()
        {
            Console.WriteLine("==================== Unicode Encoder ====================");
            (new UnicodeEncoderSample()).Run();
            Console.WriteLine("=========================================================");
        }

        private void Run()
        {
            UnicodeEncoder encoder = new UnicodeEncoder();

            string inputText = "Hi 你好";
            string unicodeText = UnicodeEncoder.Instance.Encode(inputText);
            string outputText = UnicodeEncoder.Instance.Decode(unicodeText);


            Console.WriteLine($"Text: {inputText}");
            Console.WriteLine($"Text --> Unicode: {unicodeText}");
            Console.WriteLine($"Unicode --> Text: {outputText}");
        }
    }
}
