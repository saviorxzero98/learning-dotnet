using CryptoSample.Samples;

namespace CryptoSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Base64EncoderSample.ExecuteSample();
            HexEncoderSample.ExecuteSample();
            UnicodeEncoderSample.ExecuteSample();
            HashCryptoSample.ExecuteSample();
            HmacHashCryptoSample.ExecuteSample();
            SymmetricCryptoSample.ExecuteSample();
            AsymmetricCryptoSample.ExecuteSample();
        }
    }
}
