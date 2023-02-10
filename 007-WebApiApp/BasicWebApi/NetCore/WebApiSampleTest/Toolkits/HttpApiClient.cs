using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebApiSampleTest.Toolkits
{
    public class HttpApiClient
    {
        #region Constant

        private const string TimeoutMessage = "Http Request Timeout.";

        public static class HttpHeaders
        {
            public const string ContentType = "Content-Type";
            public const string ContentEncoding = "Content-Encoding";
            public const string ContentSize = "Content-Size";
            public const string AcceptEncoding = "Accept-Encoding";
            public const string Authorization = "Authorization";
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

        /// <summary>
        /// Form File Content
        /// </summary>
        public class FormFile
        {
            /// <summary>
            /// Form Key
            /// </summary>
            public string FormKey { get; set; }

            /// <summary>
            /// File Name
            /// </summary>
            public string FileName { get; set; } = string.Empty;

            /// <summary>
            /// File Stream
            /// </summary>
            public Stream FileStream { get; set; }

            public FormFile()
            {

            }
            public FormFile(string formKey, Stream fileStream, string fileName = "")
            {
                FormKey = formKey;
                FileName = fileName;
                FileStream = fileStream;
            }
        }

        #endregion


        #region Propery

        private readonly HttpClient _client;

        #endregion


        #region Construction

        public HttpApiClient(HttpClient client)
        {
            _client = client;
        }
        public HttpApiClient(HttpClient client, int timeout)
        {
            _client = client;

            if (timeout > 0)
            {
                _client.Timeout = TimeSpan.FromSeconds(timeout);
            }
        }

        #endregion


        #region HTTP GET

        /// <summary>
        /// HTTP GET
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(string url, Dictionary<string, string> headers = null)
        {
            return GetAsync(url, headers).GetAwaiter().GetResult();
        }

        /// <summary>
        /// HTTP GET
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, string> headers = null)
        {
            return await SendAsync(HttpMethod.Get, url, headers);
        }

        #endregion


        #region HTTP POST

        /// <summary>
        /// HTTP POST
        /// </summary>
        /// <param name="url"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync(string url, Dictionary<string, string> headers = null)
        {
            return await SendAsync(HttpMethod.Post, url, headers);
        }
        /// <summary>
        /// HTTP POST
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync(string url, string data, string contentType,
                                                         Dictionary<string, string> headers = null)
        {
            return await SendAsync(HttpMethod.Post, url, data, contentType, headers);
        }

        /// <summary>
        /// HTTP POST JSON
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostJsonAsync(string url, string data, Dictionary<string, string> headers = null)
        {
            return await SendAsync(HttpMethod.Post, url, data, ContentType.Json, headers);
        }
        /// <summary>
        /// HTTP POST JSON
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostJsonAsync<T>(string url, T data, Dictionary<string, string> headers = null)
        {
            return await SendRawDataAsync(HttpMethod.Post, url, data, ContentType.Json, headers);
        }

        /// <summary>
        /// HTTP POST XML
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostXmlAsync(string url, string data, Dictionary<string, string> headers = null)
        {
            return await SendAsync(HttpMethod.Post, url, data, ContentType.Xml, headers);
        }
        /// <summary>
        /// HTTP POST XML
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostXmlAsync<T>(string url, T data, Dictionary<string, string> headers = null)
        {
            return await SendRawDataAsync(HttpMethod.Post, url, data, ContentType.Xml, headers);
        }

        /// <summary>
        /// HTTP POST Form
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostFormAsync(string url, Dictionary<string, string> form,
                                                             Dictionary<string, string> headers = null)
        {
            return await SendFormDataAsync(HttpMethod.Post, url, form, ContentType.FormUrlEncoded, headers);
        }
        /// <summary>
        /// HTTP POST Form
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostFormAsync(string url, Dictionary<string, string> form, string contentType,
                                                             Dictionary<string, string> headers = null)
        {
            return await SendFormDataAsync(HttpMethod.Post, url, form, contentType, headers);
        }

        /// <summary>
        /// HTTP POST File
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="files"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostFileAsync(string url, Dictionary<string, string> form,
                                                             Dictionary<string, KeyValuePair<string, Stream>> files,
                                                             Dictionary<string, string> headers = null)
        {
            return await SendFileAsync(HttpMethod.Post, url, form, files, headers);
        }

        /// <summary>
        /// HTTP POST File
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="files"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostFileAsync(string url, Dictionary<string, string> form,
                                                             List<FormFile> files,
                                                             Dictionary<string, string> headers = null)
        {
            return await SendFileAsync(HttpMethod.Post, url, form, files, headers);
        }

        /// <summary>
        /// HTTP POST File
        /// </summary>
        /// <param name="url"></param>
        /// <param name="files"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostFileAsync(string url, Dictionary<string, KeyValuePair<string, Stream>> files,
                                                             Dictionary<string, string> headers = null)
        {
            return await SendFileAsync(HttpMethod.Post, url, files, headers);
        }

        /// <summary>
        /// HTTP POST File
        /// </summary>
        /// <param name="url"></param>
        /// <param name="files"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostFileAsync(string url, List<FormFile> files,
                                                             Dictionary<string, string> headers = null)
        {
            return await SendFileAsync(HttpMethod.Post, url, files, headers);
        }

        #endregion


        #region HTTP PUT

        /// <summary>
        /// HTTP PUT
        /// </summary>
        /// <param name="url"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutAsync(string url, Dictionary<string, string> headers = null)
        {
            return await SendAsync(HttpMethod.Put, url, headers);
        }

        /// <summary>
        /// HTTP PUT
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutAsync(string url, string data, string contentType,
                                                         Dictionary<string, string> headers = null)
        {
            return await SendAsync(HttpMethod.Put, url, data, contentType, headers);
        }

        /// <summary>
        /// HTTP PUT JSON
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutJsonAsync(string url, string data, Dictionary<string, string> headers = null)
        {
            return await SendAsync(HttpMethod.Put, url, data, ContentType.Json, headers);
        }
        /// <summary>
        /// HTTP PUT JSON
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutJsonAsync<T>(string url, T data, Dictionary<string, string> headers = null)
        {
            return await SendRawDataAsync(HttpMethod.Put, url, data, ContentType.Json, headers);
        }

        /// <summary>
        /// HTTP PUT XML
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutXmlAsync(string url, string data, Dictionary<string, string> headers = null)
        {
            return await SendAsync(HttpMethod.Put, url, data, ContentType.Xml, headers);
        }
        /// <summary>
        /// HTTP PUT XML
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutXmlAsync<T>(string url, T data, Dictionary<string, string> headers = null)
        {
            return await SendRawDataAsync(HttpMethod.Put, url, data, ContentType.Xml, headers);
        }

        /// <summary>
        /// HTTP PUT Form
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutFormAsync(string url, Dictionary<string, string> form,
                                                             Dictionary<string, string> headers = null)
        {
            return await SendFormDataAsync(HttpMethod.Put, url, form, ContentType.FormUrlEncoded, headers);
        }
        /// <summary>
        /// HTTP PUT Form
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutFormAsync(string url, Dictionary<string, string> form, string contentType,
                                                             Dictionary<string, string> headers = null)
        {
            return await SendFormDataAsync(HttpMethod.Put, url, form, contentType, headers);
        }

        /// <summary>
        /// HTTP PUT File
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="files"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutFileAsync(string url, Dictionary<string, string> form,
                                                            Dictionary<string, KeyValuePair<string, Stream>> files,
                                                            Dictionary<string, string> headers = null)
        {
            return await SendFileAsync(HttpMethod.Put, url, form, files, headers);
        }

        /// <summary>
        /// HTTP PUT File
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="files"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutFileAsync(string url, Dictionary<string, string> form,
                                                            List<FormFile> files,
                                                            Dictionary<string, string> headers = null)
        {
            return await SendFileAsync(HttpMethod.Put, url, form, files, headers);
        }

        /// <summary>
        /// HTTP POST File
        /// </summary>
        /// <param name="url"></param>
        /// <param name="files"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutFileAsync(string url, Dictionary<string, KeyValuePair<string, Stream>> files,
                                                            Dictionary<string, string> headers = null)
        {
            return await SendFileAsync(HttpMethod.Put, url, files, headers);
        }

        /// <summary>
        /// HTTP POST File
        /// </summary>
        /// <param name="url"></param>
        /// <param name="files"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutFileAsync(string url, List<FormFile> files,
                                                            Dictionary<string, string> headers = null)
        {
            return await SendFileAsync(HttpMethod.Put, url, files, headers);
        }

        #endregion


        #region HTTP DELETE

        /// <summary>
        /// HTTP DELETE
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> DeleteAsync(string url, Dictionary<string, string> headers = null)
        {
            return await SendAsync(HttpMethod.Delete, url, headers);
        }

        #endregion


        #region HTTP PATCH

        /// <summary>
        /// HTTP PATCH
        /// </summary>
        /// <param name="url"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PatchAsync(string url, Dictionary<string, string> headers = null)
        {
            return await SendAsync(HttpExtendMethod.Patch, url, headers);
        }

        /// <summary>
        /// HTTP PATCH
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PatchAsync(string url, string data, string contentType,
                                                         Dictionary<string, string> headers = null)
        {
            return await SendAsync(HttpExtendMethod.Patch, url, data, contentType, headers);
        }

        /// <summary>
        /// HTTP PATCH JSON
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PatchJsonAsync(string url, string data, Dictionary<string, string> headers = null)
        {
            return await SendAsync(HttpExtendMethod.Patch, url, data, ContentType.Json, headers);
        }
        /// <summary>
        /// HTTP PATCH JSON
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PatchJsonAsync<T>(string url, T data, Dictionary<string, string> headers = null)
        {
            return await SendRawDataAsync(HttpExtendMethod.Patch, url, data, ContentType.Json, headers);
        }

        /// <summary>
        /// HTTP PATCH XML
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PatchXmlAsync(string url, string data, Dictionary<string, string> headers = null)
        {
            return await SendAsync(HttpExtendMethod.Patch, url, data, ContentType.Xml, headers);
        }
        /// <summary>
        /// HTTP PATCH XML
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PatchXmlAsync<T>(string url, T data, Dictionary<string, string> headers = null)
        {
            return await SendRawDataAsync(HttpExtendMethod.Patch, url, data, ContentType.Xml, headers);
        }

        /// <summary>
        /// HTTP PATCH Form
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PatchFormAsync(string url, Dictionary<string, string> form,
                                                             Dictionary<string, string> headers = null)
        {
            return await SendFormDataAsync(HttpExtendMethod.Patch, url, form, ContentType.FormUrlEncoded, headers);
        }
        /// <summary>
        /// HTTP PATCH Form
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PatchFormAsync(string url, Dictionary<string, string> form, string contentType,
                                                             Dictionary<string, string> headers = null)
        {
            return await SendFormDataAsync(HttpExtendMethod.Patch, url, form, contentType, headers);
        }

        /// <summary>
        /// HTTP PATCH File
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="files"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PatchFileAsync(string url, Dictionary<string, string> form,
                                                              Dictionary<string, KeyValuePair<string, Stream>> files,
                                                              Dictionary<string, string> headers = null)
        {
            return await SendFileAsync(HttpExtendMethod.Patch, url, form, files, headers);
        }

        /// <summary>
        /// HTTP PATCH File
        /// </summary>
        /// <param name="url"></param>
        /// <param name="form"></param>
        /// <param name="files"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PatchFileAsync(string url, Dictionary<string, string> form,
                                                              List<FormFile> files,
                                                              Dictionary<string, string> headers = null)
        {
            return await SendFileAsync(HttpExtendMethod.Patch, url, form, files, headers);
        }

        /// <summary>
        /// HTTP POST File
        /// </summary>
        /// <param name="url"></param>
        /// <param name="files"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PatchFileAsync(string url, Dictionary<string, KeyValuePair<string, Stream>> files,
                                                              Dictionary<string, string> headers = null)
        {
            return await SendFileAsync(HttpMethod.Patch, url, files, headers);
        }


        /// <summary>
        /// HTTP POST File
        /// </summary>
        /// <param name="url"></param>
        /// <param name="files"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PatchFileAsync(string url, List<FormFile> files,
                                                              Dictionary<string, string> headers = null)
        {
            return await SendFileAsync(HttpMethod.Patch, url, files, headers);
        }

        #endregion


        #region Send Request

        /// <summary>
        /// Send Request (Async)
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendAsync(HttpMethod method, string url, Dictionary<string, string> headers = null)
        {
            using (var request = new HttpRequestMessage(method, url))
            {
                try
                {
                    // Add Header
                    AddHeader(headers);

                    // Send
                    var response = await _client.SendAsync(request);
                    return response;
                }
                catch (OperationCanceledException)
                {
                    return CreateErrorResponse(HttpStatusCode.RequestTimeout, TimeoutMessage);
                }
                catch (Exception e)
                {
                    return CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                }
            }
        }

        /// <summary>
        /// Send Request
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendAsync(HttpMethod method, string url, string data, string contentType,
                                                         Dictionary<string, string> headers = null)
        {
            using (var request = new HttpRequestMessage(method, url))
            {
                try
                {
                    // Add Header
                    AddContentType(contentType);
                    AddHeader(headers);

                    // Add Body Content
                    var content = CreateHttpContent(contentType, data, Encoding.UTF8);
                    request.Content = content;

                    // Send
                    var response = await _client.SendAsync(request);
                    return response;
                }
                catch (OperationCanceledException)
                {
                    return CreateErrorResponse(HttpStatusCode.RequestTimeout, TimeoutMessage);
                }
                catch (Exception e)
                {
                    return CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                }
            }
        }

        /// <summary>
        /// Send Raw Data
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendRawDataAsync<T>(HttpMethod method, string url, T data, string contentType,
                                                                   Dictionary<string, string> headers = null)
        {
            using (var request = new HttpRequestMessage(method, url))
            {
                try
                {
                    // Add Header
                    AddContentType(contentType);
                    AddHeader(headers);

                    // Add Body Content
                    var content = CreateRawContent(contentType, data, Encoding.UTF8);
                    request.Content = content;

                    // Send
                    var response = await _client.SendAsync(request);
                    return response;
                }
                catch (OperationCanceledException)
                {
                    return CreateErrorResponse(HttpStatusCode.RequestTimeout, TimeoutMessage);
                }
                catch (Exception e)
                {
                    return CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                }
            }
        }


        /// <summary>
        /// Send Form Data
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendFormDataAsync(HttpMethod method, string url, Dictionary<string, string> data,
                                                                 string contentType,
                                                                 Dictionary<string, string> headers = null)
        {
            using (var request = new HttpRequestMessage(method, url))
            {
                try
                {
                    // Add Header
                    AddContentType(contentType);
                    AddHeader(headers);

                    // Add Body Content
                    request.Content = CreateFormContent(contentType, data);

                    // Send
                    var response = await _client.SendAsync(request);
                    return response;
                }
                catch (OperationCanceledException)
                {
                    return CreateErrorResponse(HttpStatusCode.RequestTimeout, TimeoutMessage);
                }
                catch (Exception e)
                {
                    return CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                }
            }
        }

        /// <summary>
        /// Send File
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="files"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendFileAsync(HttpMethod method, string url,
                                                             Dictionary<string, string> formData,
                                                             Dictionary<string, KeyValuePair<string, Stream>> files,
                                                             Dictionary<string, string> headers = null)
        {
            using (var request = new HttpRequestMessage(method, url))
            {
                try
                {
                    // Add Header
                    AddContentType(ContentType.FormData);
                    AddHeader(headers);

                    // Add Body Content
                    var content = CreateFormContent(formData, files);
                    request.Content = content;

                    // Send
                    var response = await _client.SendAsync(request);
                    return response;
                }
                catch (OperationCanceledException)
                {
                    return CreateErrorResponse(HttpStatusCode.RequestTimeout, TimeoutMessage);
                }
                catch (Exception e)
                {
                    return CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                }
            }
        }

        /// <summary>
        /// Send File
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="files"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendFileAsync(HttpMethod method, string url,
                                                             Dictionary<string, string> formData,
                                                             List<FormFile> files,
                                                             Dictionary<string, string> headers = null)
        {
            using (var request = new HttpRequestMessage(method, url))
            {
                try
                {
                    // Add Header
                    AddContentType(ContentType.FormData);
                    AddHeader(headers);

                    // Add Body Content
                    var content = CreateFormContent(formData, files);
                    request.Content = content;

                    // Send
                    var response = await _client.SendAsync(request);
                    return response;
                }
                catch (OperationCanceledException)
                {
                    return CreateErrorResponse(HttpStatusCode.RequestTimeout, TimeoutMessage);
                }
                catch (Exception e)
                {
                    return CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                }
            }
        }

        /// <summary>
        /// Send File
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="files"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendFileAsync(HttpMethod method, string url,
                                                             Dictionary<string, KeyValuePair<string, Stream>> files,
                                                             Dictionary<string, string> headers = null)
        {
            using (var request = new HttpRequestMessage(method, url))
            {
                try
                {
                    // Add Header
                    AddContentType(ContentType.FormData);
                    AddHeader(headers);

                    // Add Body Content
                    var content = CreateFileContent(files);
                    request.Content = content;

                    // Send
                    var response = await _client.SendAsync(request);
                    return response;
                }
                catch (OperationCanceledException)
                {
                    return CreateErrorResponse(HttpStatusCode.RequestTimeout, TimeoutMessage);
                }
                catch (Exception e)
                {
                    return CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                }
            }
        }

        /// <summary>
        /// Send File
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="files"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SendFileAsync(HttpMethod method, string url,
                                                             List<FormFile> files,
                                                             Dictionary<string, string> headers = null)
        {
            using (var request = new HttpRequestMessage(method, url))
            {
                try
                {
                    // Add Header
                    AddContentType(ContentType.FormData);
                    AddHeader(headers);

                    // Add Body Content
                    var content = CreateFileContent(files);
                    request.Content = content;

                    // Send
                    var response = await _client.SendAsync(request);
                    return response;
                }
                catch (OperationCanceledException)
                {
                    return CreateErrorResponse(HttpStatusCode.RequestTimeout, TimeoutMessage);
                }
                catch (Exception e)
                {
                    return CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                }
            }
        }

        #endregion


        #region Header

        /// <summary>
        /// Add Header
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        protected void AddContentType(string type)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(type));
        }

        /// <summary>
        /// Add Header
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        protected void AddHeader(string key, string value)
        {
            if (CheckHttpAuthHeader(key, value))
            {
                _client.DefaultRequestHeaders.Add(key, value);
            }
        }

        /// <summary>
        /// Add Header
        /// </summary>
        /// <param name="headers"></param>
        protected void AddHeader(Dictionary<string, string> headers = null)
        {
            if (headers != null)
            {
                List<string> headerKeys = new List<string>(headers.Keys);
                foreach (string key in headerKeys)
                {
                    string value = headers[key];
                    AddHeader(key, value);
                }
            }
        }

        /// <summary>
        /// Check Header
        /// </summary>
        /// <param name="authKey"></param>
        /// <param name="authValue"></param>
        /// <returns></returns>
        protected bool CheckHttpAuthHeader(string authKey, string authValue)
        {
            if (string.IsNullOrEmpty(authKey) || string.IsNullOrEmpty(authValue))
            {
                return false;
            }
            return true;
        }

        #endregion


        #region Body

        /// <summary>
        /// 建立 Body
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="rawData"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        protected HttpContent CreateHttpContent(string contentType, string rawData, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(rawData))
            {
                return null;
            }

            return new StringContent(rawData, encoding ?? Encoding.UTF8, contentType);
        }

        /// <summary>
        /// 建立 Body (Raw 內容)
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="rawData"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        protected HttpContent CreateRawContent<T>(string contentType, T rawData, Encoding encoding = null)
        {
            if (rawData == null)
            {
                return null;
            }

            switch (contentType.ToLower())
            {
                case ContentType.Plain:
                case ContentType.Html:
                case ContentType.Json:
                case ContentType.JavaScript:
                    string text = JToken.FromObject(rawData).ToString();
                    return CreateHttpContent(contentType.ToLower(), text, encoding);

                case ContentType.XmlFile:
                case ContentType.Xml:
                    JToken jData = JToken.FromObject(rawData);
                    if (jData.Type == JTokenType.String)
                    {
                        string xml = jData.ToString();
                        return CreateHttpContent(contentType.ToLower(), xml, encoding);
                    }
                    else if (jData.Type == JTokenType.Object)
                    {
                        var xml = XmlConverter.SerializeObject(rawData);
                        return CreateHttpContent(contentType.ToLower(), xml, encoding);
                    }
                    break;
            }

            return null;
        }

        /// <summary>
        /// 建立 Body 內容 (Multipart Form Data / Form Url Encoded)
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        protected HttpContent CreateFormContent(string contentType, Dictionary<string, string> formData)
        {
            if (formData == null)
            {
                return null;
            }

            switch (contentType.ToLower())
            {
                case ContentType.FormData:
                    var multipartContent = new MultipartFormDataContent();
                    var keys = new List<string>(formData.Keys);
                    foreach (var key in keys)
                    {
                        multipartContent.Add(new StringContent(formData[key]), key);
                    }
                    return multipartContent;

                case ContentType.FormUrlEncoded:
                    var forms = formData.Select(d => d).ToList();
                    return new FormUrlEncodedContent(forms);
            }

            return null;
        }

        /// <summary>
        /// 建立 Body 內容 (Multipart Form Data)
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="formData"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        protected HttpContent CreateFormContent(Dictionary<string, string> formData,
                                                Dictionary<string, KeyValuePair<string, Stream>> files)
        {
            var content = new MultipartFormDataContent();

            // Form Data
            if (formData != null)
            {
                var dataKeys = new List<string>(formData.Keys);
                foreach (var dataKey in dataKeys)
                {
                    content.Add(new StringContent(formData[dataKey]), dataKey);
                }
            }

            // File Data
            if (files != null)
            {
                var fileKeys = new List<string>(files.Keys);
                foreach (var key in fileKeys)
                {
                    Stream fileStream = files[key].Value;
                    string fileName = files[key].Key;
                    content.Add(new StreamContent(fileStream), key, fileName);
                }
            }

            return content;
        }

        /// <summary>
        /// 建立 Body 內容 (Multipart Form Data)
        /// </summary>
        /// <param name="formData"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        protected HttpContent CreateFormContent(Dictionary<string, string> formData, List<FormFile> files)
        {
            var content = new MultipartFormDataContent();

            // Form Data
            if (formData != null)
            {
                var dataKeys = new List<string>(formData.Keys);
                foreach (var dataKey in dataKeys)
                {
                    content.Add(new StringContent(formData[dataKey]), dataKey);
                }
            }

            // File Data
            if (files != null)
            {
                foreach (var file in files)
                {
                    string key = file.FormKey;
                    Stream fileStream = file.FileStream;
                    string fileName = file.FileName;
                    content.Add(new StreamContent(fileStream), key, fileName);
                }
            }

            return content;
        }

        /// <summary>
        /// 建立 Body 內容 (Multipart Form Data)
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        protected HttpContent CreateFileContent(Dictionary<string, KeyValuePair<string, Stream>> files)
        {
            var content = new MultipartFormDataContent();

            // File Data
            if (files != null)
            {
                var fileKeys = new List<string>(files.Keys);
                foreach (var key in fileKeys)
                {
                    Stream fileStream = files[key].Value;
                    string fileName = files[key].Key;

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        content.Add(new StreamContent(fileStream), key, fileName);
                    }
                    else
                    {
                        content.Add(new StreamContent(fileStream), key);
                    }
                }
            }

            return content;
        }

        /// <summary>
        /// 建立 Body 內容 (Multipart Form Data)
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        protected HttpContent CreateFileContent(List<FormFile> files)
        {
            var content = new MultipartFormDataContent();

            // File Data
            if (files != null)
            {
                foreach (var file in files)
                {
                    string key = file.FormKey;
                    Stream fileStream = file.FileStream;
                    string fileName = file.FileName;

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        content.Add(new StreamContent(fileStream), key, fileName);
                    }
                    else
                    {
                        content.Add(new StreamContent(fileStream), key);
                    }
                }
            }

            return content;
        }


        #endregion


        #region Response

        /// <summary>
        /// Create Error Response
        /// </summary>
        /// <param name="code"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        protected HttpResponseMessage CreateErrorResponse(HttpStatusCode code, string errorMessage)
        {
            return new HttpResponseMessage(code)
            {
                Content = CreateHttpContent(ContentType.Plain, errorMessage),
                ReasonPhrase = errorMessage
            };
        }

        /// <summary>
        /// Create Error Response
        /// </summary>
        /// <param name="code"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected HttpResponseMessage CreateErrorResponse(HttpStatusCode code, Exception e)
        {
            string errorMessage = (e == null) ? code.ToString() : e.Message;

            return new HttpResponseMessage(code)
            {
                Content = CreateHttpContent(ContentType.Plain, errorMessage),
                ReasonPhrase = errorMessage
            };
        }

        #endregion


        #region Other

        private const string Bearer = "bearer";

        /// <summary>
        /// 加入 Bearer Header
        /// </summary>
        /// <param name="token"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static Dictionary<string, string> AddBearerAuthHeader(string token, Dictionary<string, string> headers = null)
        {
            var newHeaders = headers ?? new Dictionary<string, string>();

            if (token.StartsWith($"{Bearer} ", StringComparison.InvariantCultureIgnoreCase))
            {
                newHeaders.Add(HttpHeaders.Authorization, token);
            }
            else
            {
                newHeaders.Add(HttpHeaders.Authorization, $"{Bearer} {token}");
            }
            return newHeaders;
        }

        #endregion
    }
}
