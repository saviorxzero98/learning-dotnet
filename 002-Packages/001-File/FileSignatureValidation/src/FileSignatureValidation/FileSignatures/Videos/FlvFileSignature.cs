using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Videos
{
    public static class FlvFileSignature
    {
        public const string Extension = ".flv";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x46, 0x4c, 0x56 })
                };
            }
        }
    }
}
