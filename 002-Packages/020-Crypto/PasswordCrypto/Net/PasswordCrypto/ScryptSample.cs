using Scrypt;

namespace PasswordCrypto
{
    public class ScryptSample
    {
        public static void Demo()
        {
            Console.WriteLine("===== Demo Scrypt =====");

            var textList = new List<string>()
            {
                "123456789",
                "123456789",
                "123456789"
            };

            // Hash
            foreach (var text in textList)
            {
                Console.WriteLine($"Plain text: \"{text}\"");

                var start = DateTime.Now;

                var hashText = Hash(text);

                var end = DateTime.Now;

                Console.WriteLine($"Hash text: \"{hashText}\" ({(end - start).TotalMilliseconds} ms)");
            }

            Console.WriteLine("");
        }

        private static string Hash(string password)
        {
            var encoder = new ScryptEncoder();
            return encoder.Encode(password);
        }

        private static bool Verify(string password, string hashText)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            return encoder.Compare(password, hashText);
        }
    }
}
