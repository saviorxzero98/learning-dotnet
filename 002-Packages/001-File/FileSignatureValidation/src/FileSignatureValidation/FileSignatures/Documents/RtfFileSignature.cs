using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Documents
{
    public static class RtfFileSignature
    {
        public const string Extension = ".rtf";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x7B, 0x5C, 0x72, 0x74, 0x66, 0x31 })
                };
            }
        }
    }
}
