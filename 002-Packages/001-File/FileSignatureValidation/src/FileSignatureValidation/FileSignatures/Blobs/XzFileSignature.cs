using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Blobs
{
    public static class XzFileSignature
    {
        public const string Extension = ".xz";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0xFD, 0x37, 0x3A, 0x58, 0x5A, 0x00 })
                };
            }
        }
    }
}
