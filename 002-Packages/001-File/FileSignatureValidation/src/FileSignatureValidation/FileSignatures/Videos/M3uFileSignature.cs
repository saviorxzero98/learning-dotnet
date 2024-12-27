using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Videos
{
    public class M3uFileSignature
    {
        public const string Extension = ".m3u";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x23, 0x45, 0x58, 0x54, 0x4d, 0x33, 0x55 })
                };
            }
        }
    }
}
