using Refit;
using RefitWebApiCore.RestServices;

namespace RefitWebApiClient
{
    public class RefitRestServiceFactory : IRefiRestServiceFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RefitRestServiceFactory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 建立 Rest Service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public T Create<T>(RefitHttpApiHeaderContext? context = null) where T : IRefitRestService
        {
            // 建立 Http Client
            var httpClient = _httpClientFactory.CreateClient(IRefiRestServiceFactory.ClientName);
            httpClient = SetDefaultHeaders(httpClient, context);

            // 建立 Refit REST Service
            var settings = new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer()
            };
            var restService = RestService.For<T>(httpClient, settings);
            return restService;
        }

        /// <summary>
        /// 設定 Headers
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="headerContext"></param>
        /// <returns></returns>
        protected virtual HttpClient SetDefaultHeaders(HttpClient httpClient, RefitHttpApiHeaderContext? headerContext = null)
        {
            if (headerContext == null)
            {
                return httpClient;
            }

            if (!string.IsNullOrEmpty(headerContext?.Authorization))
            {
                httpClient.DefaultRequestHeaders.Authorization = headerContext.GetAuthorizationValue();
            }

            if (headerContext.Headers != null)
            {
                foreach (var header in headerContext.Headers)
                {
                    var key = header.Key;
                    var value = header.Value;
                    httpClient.DefaultRequestHeaders.Add(key, value);
                }
            }
            return httpClient;
        }
    }
}
