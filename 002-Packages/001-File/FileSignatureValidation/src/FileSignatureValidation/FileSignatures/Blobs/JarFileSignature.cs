using System;
using System.Collections.Generic;
using System.Text;

namespace FileSignatureValidation.FileSignatures.Blobs
{
    public static class JarFileSignature
    {
        public const string Extension = ".jar";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x50, 0x4B, 0x03, 0x04 }),
                    new FileSignature(Extension, new byte[] { 0x50, 0x4B, 0x05, 0x06 }),
                    new FileSignature(Extension, new byte[] { 0x50, 0x4B, 0x07, 0x08 })
                };
            }
        }
    }
}
