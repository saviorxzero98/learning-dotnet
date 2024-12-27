using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Documents
{
    public static class PdfFileSignature
    {
        public const string Extension = ".pdf";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x25, 0x50, 0x44, 0x46 })
                };
            }
        }
    }
}
