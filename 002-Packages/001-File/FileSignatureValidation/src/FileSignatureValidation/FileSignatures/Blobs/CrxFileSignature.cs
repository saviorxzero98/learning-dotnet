using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Blobs
{
    public static class CrxFileSignature
    {
        public const string Extension = ".crx";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x43, 0x72, 0x32, 0x34 })
                };
            }
        }
    }
}
