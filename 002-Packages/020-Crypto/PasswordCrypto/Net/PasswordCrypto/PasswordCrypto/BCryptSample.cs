namespace PasswordCrypto
{
    public static class BCryptSample
    {
        public static void Demo()
        {
            Console.WriteLine("===== Demo BCrypt =====");

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

                var hashText = Hash(text);

                var end = DateTime.Now;

                Console.WriteLine($"Hash text: \"{hashText}\" ({(end - start).TotalMilliseconds} ms)");
            }

            Console.WriteLine("");
        }

        private static string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private static bool Verify(string password, string hashText)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashText);
        }
    }
}
