using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Blobs
{
    public static class ZipFileSignature
    {
        public const string Extension = ".zip";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x50, 0x4B, 0x03, 0x04 }),
                    new FileSignature(Extension, new byte[] { 0x50, 0x4B, 0x4C, 0x49, 0x54, 0x55 }),
                    new FileSignature(Extension, new byte[] { 0x50, 0x4B, 0x53, 0x70, 0x58 }),
                    new FileSignature(Extension, new byte[] { 0x50, 0x4B, 0x05, 0x06 }),
                    new FileSignature(Extension, new byte[] { 0x50, 0x4B, 0x07, 0x08 }),
                    new FileSignature(Extension, new byte[] { 0x57, 0x69, 0x6E, 0x5A, 0x69, 0x70 })
                };
            }
        }
    }
}
