using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Blobs
{
    public static class GzFileSignature
    {
        public const string Extension = ".gz";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x1F, 0x8B })
                };
            }
        }
    }
}
