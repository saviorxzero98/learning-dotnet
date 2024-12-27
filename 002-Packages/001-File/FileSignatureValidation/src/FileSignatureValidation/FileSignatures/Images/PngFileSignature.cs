using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Images
{
    public static class PngFileSignature
    {
        public const string Extension = ".png";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x89, 0x50, 0x4E, 0x47 })
                };
            }
        }
    }
}
