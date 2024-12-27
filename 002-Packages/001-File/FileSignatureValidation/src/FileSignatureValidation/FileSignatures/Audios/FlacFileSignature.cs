using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Audios
{
    public static class FlacFileSignature
    {
        public const string Extension = ".flac";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x66, 0x4c, 0x61, 0x43, 0x00, 0x00, 0x00, 0x22 })
                };
            }
        }
    }
}
