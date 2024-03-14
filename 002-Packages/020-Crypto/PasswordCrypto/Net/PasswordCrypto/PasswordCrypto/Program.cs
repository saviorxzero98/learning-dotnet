namespace PasswordCrypto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PBKDF2Sample.Demo();
            BCryptSample.Demo();
            ScryptSample.Demo();
            HashSample.Demo();
        }
    }
}
