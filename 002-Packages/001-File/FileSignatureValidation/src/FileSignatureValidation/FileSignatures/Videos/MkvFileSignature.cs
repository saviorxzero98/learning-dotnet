using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Videos
{
    public static class MkvFileSignature
    {
        public const string Extension = ".mkv";

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
