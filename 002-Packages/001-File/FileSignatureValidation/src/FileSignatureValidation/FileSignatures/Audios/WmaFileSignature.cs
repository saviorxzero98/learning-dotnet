using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Audios
{
    public static class WmaFileSignature
    {
        public const string Extension = ".wma";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x30, 0x26, 0xB2, 0x75, 0x8E, 0x66, 0xCF, 0x11 })
                };
            }
        }
    }
}
