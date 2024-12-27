using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Blobs
{
    public static class TarFileSignature
    {
        public const string Extension = ".tar";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x75, 0x73, 0x74, 0x61, 0x72, 0x00, 0x30, 0x30 }),
                    new FileSignature(Extension, new byte[] { 0x75, 0x73, 0x74, 0x61, 0x72, 0x20, 0x20, 0x00 })
                };
            }
        }
    }
}
