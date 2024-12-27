using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Images
{
    public class WebPFileSignature
    {
        public const string Extension = ".webp";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x52, 0x49, 0x46, 0x46 }, 0, 8, new byte[] { 0x57, 0x45, 0x42, 0x50 })
                };
            }
        }
    }
}
