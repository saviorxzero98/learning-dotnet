using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Videos
{
    public static class MpegFileSignature
    {
        public const string Extension = ".mpeg";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x00, 0x00, 0x01, 0xBA }),
                    new FileSignature(Extension, new byte[] { 0x00, 0x00, 0x01, 0xB3 })
                };
            }
        }
    }
}
