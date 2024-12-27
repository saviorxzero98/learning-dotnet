using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Videos
{
    public static class Mp4FileSignature
    {
        public const string Extension = ".mp4";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x66, 0x74, 0x79, 0x70 })
                };
            }
        }
    }
}
