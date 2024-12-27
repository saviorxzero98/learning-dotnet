using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Videos
{
    public static class RmFileSignature
    {
        public const string Extension = ".rm";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x2e, 0x52, 0x4d, 0x46 })
                };
            }
        }
    }
}
