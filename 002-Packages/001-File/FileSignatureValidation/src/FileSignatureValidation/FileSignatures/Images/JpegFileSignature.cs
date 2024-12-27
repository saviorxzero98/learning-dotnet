using System.Collections.Generic;

namespace FileSignatureValidation.FileSignatures.Images
{
    public class JpegFileSignature
    {
        public const string Extension = ".jpeg";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 }),
                    new FileSignature(Extension, new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 }),
                    new FileSignature(Extension, new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 }),
                    new FileSignature(Extension, new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 }),
                    new FileSignature(Extension, new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 }),
                    new FileSignature(Extension, new byte[] { 0xFF, 0xD8, 0xFF, 0xDB }),
                    new FileSignature(Extension, new byte[] { 0xFF, 0xD8, 0xFF, 0xEE })
                };
            }
        }
    }
}
