using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Images
{
    public static class BmpFileSignature
    {
        public const string Extension = ".bmp";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x42, 0x4D })
                };
            }
        }
    }
}
