using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Documents
{
    public static class XlsFileSignature
    {
        public const string Extension = ".xls";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }),
                    new FileSignature(Extension, new byte[] { 0x09, 0x08, 0x10, 0x00, 0x00, 0x06, 0x05, 0x00 }),
                    new FileSignature(Extension, new byte[] { 0xFD, 0xFF, 0xFF, 0xFF })
                };
            }
        }
    }
}
