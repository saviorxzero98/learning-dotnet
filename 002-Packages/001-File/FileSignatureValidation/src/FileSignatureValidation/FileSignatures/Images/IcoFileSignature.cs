using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Images
{
    public static class IcoFileSignature
    {
        public const string Extension = ".ico";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x00, 0x00, 0x01, 0x00 })
                };
            }
        }
    }
}
