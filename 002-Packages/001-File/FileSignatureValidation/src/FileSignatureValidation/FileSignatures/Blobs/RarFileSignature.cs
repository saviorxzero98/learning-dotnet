using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Blobs
{
    public static class RarFileSignature
    {
        public const string Extension = ".rar";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x00 })
                };
            }
        }
    }
}
