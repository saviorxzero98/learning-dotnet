using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Audios
{
    public static class Mp3FileSignature
    {
        public const string Extension = ".mp3";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x49, 0x44, 0x33 }),
                    new FileSignature(Extension, new byte[] { 0xFF, 0xFB }),
                    new FileSignature(Extension, new byte[] { 0xFF, 0xF3 }),
                    new FileSignature(Extension, new byte[] { 0xFF, 0xF2 })
                };
            }
        }
    }
}
