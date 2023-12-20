using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WireMock;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace MockWebApiTestSample
{
    public class MockWebApiTest : IDisposable
    {
        protected WireMockServer MockServer;

        public const int ServerPort = 655;

        public const bool IsHttps = false;

        // Setup
        public MockWebApiTest()
        {
            MockServer = WireMockServer.Start(ServerPort, IsHttps);
        }

        // Teardown
        public void Dispose()
        {
            if (MockServer != null && MockServer.IsStarted)
            {
                MockServer.Stop();
            }
        }

        [Fact]
        public async Task TestGetMockWebApi()
        {
            // Arrange
            var model = new HttpResponseModel()
            {
                StatusCode = 200,
                Message = "Test Ok"
            };
            var requestBuilder = CreateRequest(HttpMethod.Get, "/api/test");
            var responseBuilder = CreateJsonResponse(HttpStatusCode.OK, model);
            AddRouter(requestBuilder, responseBuilder);

            // Act
            var client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:{ServerPort}/api/test");
            var responseRaw = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<HttpResponseModel>(responseRaw);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(model.Message, responseModel.Message);
        }

        [Fact]
        public async Task TestPostMockWebApi()
        {
            // Arrange
            var model = new HttpResponseModel()
            {
                StatusCode = 200,
                Message = "Test Ok"
            };
            var requestBuilder = CreateRequest(HttpMethod.Post, "/api/test");
            var responseBuilder = CreateJsonResponse(HttpStatusCode.OK, model);
            AddRouter(requestBuilder, responseBuilder);

            // Act
            var client = new HttpClient();
            var data = new HttpRequestModel() { Id = 1, Name = "Alice" };
            var body = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"http://localhost:{ServerPort}/api/test", body);
            var responseRaw = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<HttpResponseModel>(responseRaw);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(model.Message, responseModel.Message);
        }

        [Fact]
        public async Task TestPostMockWebApi2()
        {
            // Arrange
            var model = new HttpResponseModel()
            {
                StatusCode = 200,
                Message = "Test Ok"
            };
            var requestBuilder = CreateRequest(HttpMethod.Post, "/api/test");
            var responseBuilder = CreateJsonResponse(HttpStatusCode.OK, (request => 
            {
                return model;            
            }));
            AddRouter(requestBuilder, responseBuilder);

            // Act
            var client = new HttpClient();
            var data = new HttpRequestModel() { Id = 1, Name = "Alice" };
            var body = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"http://localhost:{ServerPort}/api/test", body);
            var responseRaw = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<HttpResponseModel>(responseRaw);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(model.Message, responseModel.Message);
        }


        #region Common

        protected void AddRouter(IRequestBuilder requset, IResponseBuilder response)
        {
            MockServer.Given(requset)
                   .RespondWith(response);
        }

        protected IRequestBuilder CreateRequest(HttpMethod httpMethod, string router)
        {
            var builder = Request.Create()
                                 .WithPath(router);

            switch (httpMethod.Method.ToUpper())
            {
                case "GET":
                    builder = builder.UsingGet();
                    break;

                case "POST":
                    builder = builder.UsingPost();
                    builder = builder.WithBody((object body) =>
                    {
                        return (body != null);
                    });
                    break;

                case "PUT":
                    builder = builder.UsingPut();
                    builder = builder.WithBody((object body) =>
                    {
                        return (body != null);
                    });
                    break;

                case "PATCH":
                    builder = builder.UsingPatch();
                    builder = builder.WithBody((object body) =>
                    {
                        return (body != null);
                    });
                    break;

                case "DELETE":
                    builder = builder.UsingDelete();
                    break;

                default:
                    builder = builder.UsingMethod(httpMethod.Method.ToUpper());
                    break;
            }
            return builder;
        }

        protected IResponseBuilder CreateResponse(HttpStatusCode statusCode = HttpStatusCode.OK, string body = "")
        {
            var builder = Response.Create();
            builder = builder.WithStatusCode(statusCode);

            if (!string.IsNullOrEmpty(body))
            {
                builder = builder.WithBody(body);
            }
            return builder;
        }

        protected IResponseBuilder CreateResponse(HttpStatusCode statusCode, Func<IRequestMessage, string> bodyFactory)
        {
            var builder = Response.Create();
            builder = builder.WithStatusCode(statusCode);
            builder = builder.WithBody(bodyFactory);
            return builder;
        }

        protected IResponseBuilder CreateJsonResponse(HttpStatusCode statusCode = HttpStatusCode.OK, object body = null)
        {
            var builder = Response.Create();
            builder = builder.WithStatusCode(statusCode);

            if (body != null)
            {
                builder = builder.WithBodyAsJson(body);
            }
            return builder;
        }

        protected IResponseBuilder CreateJsonResponse(HttpStatusCode statusCode, Func<IRequestMessage, object> bodyFactory)
        {
            var builder = Response.Create();
            builder = builder.WithStatusCode(statusCode);
            builder = builder.WithBody((request) => 
            {
                var bodyData = bodyFactory.Invoke(request);
                return JsonConvert.SerializeObject(bodyData);
            });
            return builder;
        }

        #endregion
    }
}
