using System.Net.Http;

namespace HttpApiClientToolkit
{
    public static class HttpHeaders
    {
        public const string ContentType = "Content-Type";
        public const string ContentEncoding = "Content-Encoding";
        public const string ContentSize = "Content-Size";
        public const string AcceptEncoding = "Accept-Encoding";
        public const string Authorization = "Authorization";

        // Response
        public const string SetCookie = "Set-Cookie";
    }

    public static class ContentType
    {
        public const string Plain = "text/plain";
        public const string Html = "text/html";
        public const string XmlFile = "text/xml";
        public const string Json = "application/json";
        public const string Xml = "application/xml";
        public const string JavaScript = "application/javascript";
        public const string FormUrlEncoded = "application/x-www-form-urlencoded";
        public const string FormData = "multipart/form-data";
    }

    public static class ContentEncoding
    {
        public const string GZip = "gzip";
        public const string Deflate = "deflate";
        public const string Brotli = "br";

        public const string Compress = "compress";
        public const string Identity = "identity";
    }

    public static class HttpMethodType
    {
        public const string Get = "GET";
        public const string Post = "POST";
        public const string Put = "PUT";
        public const string Patch = "PATCH";
        public const string Delete = "DELETE";
    }

    public static class HttpExtendMethod
    {
        public static HttpMethod Patch
        {
            get
            {
                return new HttpMethod(HttpMethodType.Patch);
            }
        }
    }
}
