using Microsoft.AspNetCore.ResponseCompression;
using System.IO;
using System.IO.Compression;

namespace WebApiSample.CompressionProviders
{
    public class DeflateCompressionProvider : ICompressionProvider
    {
        public const string Name = "deflate";
        public string EncodingName => Name;

        public bool SupportsFlush => true;

        public Stream CreateStream(Stream outputStream)
        {
            return new DeflateStream(outputStream, CompressionMode.Compress);
        }
    }
}
