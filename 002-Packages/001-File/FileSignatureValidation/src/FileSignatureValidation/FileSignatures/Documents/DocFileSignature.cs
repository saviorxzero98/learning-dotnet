using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Documents
{
    public static class DocFileSignature
    {
        public const string Extension = ".doc";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 })
                };
            }
        }
    }
}
