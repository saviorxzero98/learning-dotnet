using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Blobs
{
    public static class SysFileSignature
    {
        public const string Extension = ".sys";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x4d, 0x5a })
                };
            }
        }
    }
}
