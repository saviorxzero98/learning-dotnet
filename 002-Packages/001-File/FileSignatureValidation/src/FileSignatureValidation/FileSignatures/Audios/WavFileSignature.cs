using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Audios
{
    public static class WavFileSignature
    {
        public const string Extension = ".wav";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x52, 0x49, 0x46, 0x46 })
                };
            }
        }
    }
}
