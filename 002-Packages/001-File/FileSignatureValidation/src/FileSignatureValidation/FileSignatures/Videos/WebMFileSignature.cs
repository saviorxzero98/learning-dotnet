using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Videos
{
    public static class WebMFileSignature
    {
        public const string Extension = ".webm";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x1A, 0x45, 0xDF, 0xA3 })
                };
            }
        }
    }
}
