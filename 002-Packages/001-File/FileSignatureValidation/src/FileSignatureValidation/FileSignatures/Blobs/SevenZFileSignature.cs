using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Blobs
{
    public static class SevenZFileSignature
    {
        public const string Extension = ".7z";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x37, 0x7A, 0xBC, 0xAF, 0x27, 0x1C })
                };
            }
        }
    }
}
