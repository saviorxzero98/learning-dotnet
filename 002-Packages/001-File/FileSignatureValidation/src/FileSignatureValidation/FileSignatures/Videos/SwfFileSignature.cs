using System;
using System.Collections.Generic;
using System.Text;

namespace FileSignatureValidation.FileSignatures.Videos
{
    public static class SwfFileSignature
    {
        public const string Extension = ".swf";

        public static List<FileSignature> Signatures
        {
            get
            {
                return new List<FileSignature>()
                {
                    new FileSignature(Extension, new byte[] { 0x46, 0x57, 0x53 }),
                    new FileSignature(Extension, new byte[] { 0x43, 0x57, 0x53 })
                };
            }
        }
    }
}
