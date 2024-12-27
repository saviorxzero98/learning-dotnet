using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Images
{
    public static class GifFileSignature
    {
        public const string Extension = ".gif";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x47, 0x49, 0x46, 0x38 })
                };
            }
        }
    }
}
