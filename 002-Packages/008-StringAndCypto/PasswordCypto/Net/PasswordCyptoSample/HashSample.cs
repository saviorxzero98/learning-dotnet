using System.Security.Cryptography;
using System.Text;

namespace PasswordCyptoSample
{
    public class HashSample
    {
        public enum HashAlgorithmType { MD5, SHA1, SHA256, SHA384, SHA512 }

        public static void Demo()
        {
            Console.WriteLine("===== Demo SHA384 (no salts) =====");

            var textList = new List<string>()
            {
                "",
                "123456789",
                "123456789",
                "123456789"
            };

            // Hash
            foreach (var text in textList)
            {
                Console.WriteLine($"Plain text: \"{text}\"");

                var start = DateTime.Now;

                var hashText = Hash(text, HashAlgorithmType.SHA384);

                var end = DateTime.Now;

                Console.WriteLine($"Hash text: \"{hashText}\" ({(end - start).TotalMilliseconds} ms)");
            }

            Console.WriteLine("");
        }

        private static string Hash(string password, HashAlgorithmType type = HashAlgorithmType.SHA256)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            HashAlgorithm algorithm = GetHashAlgorithm(type);
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] crypto = algorithm.ComputeHash(bytes);

            return Convert.ToBase64String(crypto);
        }

        private static bool Verify(string password, string hashText, HashAlgorithmType type = HashAlgorithmType.SHA256)
        {
            var passwordHash = Hash(password, type);
            return passwordHash == hashText;
        }


        private static HashAlgorithm GetHashAlgorithm(HashAlgorithmType algorithm)
        {
            switch (algorithm)
            {
                case HashAlgorithmType.SHA1:
                    return SHA1.Create();
                case HashAlgorithmType.SHA256:
                    return SHA256.Create();
                case HashAlgorithmType.SHA384:
                    return SHA384.Create();
                case HashAlgorithmType.SHA512:
                    return SHA512.Create();
                case HashAlgorithmType.MD5:
                default:
                    return MD5.Create();
            }
        }
    }
}
