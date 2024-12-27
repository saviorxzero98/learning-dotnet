using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Blobs
{
    public static class DllFileSignature
    {
        public const string Extension = ".dll";

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
