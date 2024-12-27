using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Images
{
    public static class TiffFileSignature
    {
        public const string Extension = ".tiff";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x49, 0x49, 0x2A, 0x00 }),
                    new FileSignature(Extension, new byte[] { 0x4D, 0x4D, 0x00, 0x2A }),
                    new FileSignature(Extension, new byte[] { 0x4D, 0x4D, 0x00, 0x2B }),
                };
            }
        }
    }
}
