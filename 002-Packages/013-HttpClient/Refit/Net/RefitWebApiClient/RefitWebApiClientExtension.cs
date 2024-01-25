using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using Refit;
using RefitWebApiClient.HeaderHandlers;
using RefitWebApiCore.AppServices;

namespace RefitWebApiClient
{
    public static class RefitWebApiClientExtension
    {
        public const string DefaultClientName = "RefitApiClient";

        /// <summary>
        /// 加入 Http Client
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settings"></param>
        /// <param name="httpClientName"></param>
        /// <returns></returns>
        public static ServiceCollection AddHttpApiClient(this ServiceCollection services,
                                                         RefitApiSetting settings,
                                                         string httpClientName = DefaultClientName)
        {
            // 加入 Http Client
            AddHttpClient(services, settings, httpClientName);

            // 加入 API Header 設定器
            AddApiHeaderContextAccessor(services);

            // 加入 API Client
            AddRefitClient<IBookAppService>(services, settings, httpClientName);
            AddRefitClient<IUserAppService>(services, settings, httpClientName);

            return services;
        }

        /// <summary>
        /// 加入 Http Client
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settings"></param>
        /// <param name="httpClientName"></param>
        /// <returns></returns>
        private static IHttpClientBuilder AddHttpClient(ServiceCollection services,
                                                        RefitApiSetting settings,
                                                        string httpClientName)
        {
            // 加入 Http Clinet
            var builder = services.AddHttpClient(httpClientName, (httpClient) =>
            {
                httpClient.BaseAddress = new Uri(settings.HostUrl);
            });

            // 忽略 Https 憑證檢查
            if (settings != null && !settings.CertificateValidation)
            {
                var handler = new HttpClientHandler()
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
                };
                builder.ConfigurePrimaryHttpMessageHandler(() => handler);
            }

            return builder;
        }

        /// <summary>
        /// 加入 API Header 設定器
        /// </summary>
        /// <param name="services"></param>
        private static void AddApiHeaderContextAccessor(ServiceCollection services)
        {
            services.AddSingleton<ContextAccessor<GenericApiHeaderContext>>();
            services.AddSingleton<IContextSetter<GenericApiHeaderContext>>(p => p.GetRequiredService<ContextAccessor<GenericApiHeaderContext>>());
            services.AddSingleton<IContextGetter<GenericApiHeaderContext>>(p => p.GetRequiredService<ContextAccessor<GenericApiHeaderContext>>());
        }


        /// <summary>
        /// 加入 Refit Client
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="settings"></param>
        /// <param name="httpClientName"></param>
        /// <returns></returns>
        private static IHttpClientBuilder AddRefitClient<T>(ServiceCollection services,
                                                            RefitApiSetting settings,
                                                            string httpClientName) where T : class
        {
            var builder = services.AddRefitClient<T>(GetRefitSettings(), httpClientName)
                                  .AddRetryPolicy(settings)
                                  .AddTimeoutPolicy(settings);
            return builder;
        }

        /// <summary>
        /// 取得 Refit Client Setting
        /// </summary>
        /// <returns></returns>
        private static Func<IServiceProvider, RefitSettings?> GetRefitSettings()
        {
            return (services) =>
            {
                var contextGetter = services.GetRequiredService<IContextGetter<GenericApiHeaderContext>>();
                var configuration = services.GetRequiredService<IConfiguration>();
                var settings = new RefitSettings
                {
                    ContentSerializer = new NewtonsoftJsonContentSerializer(),
                    HttpMessageHandlerFactory = () =>
                        new GenericApiHeaderHandler(contextGetter, configuration)
                        {
                            InnerHandler = new SocketsHttpHandler()
                        }
                };
                return settings;
            };
        }

        /// <summary>
        /// 設定重試原則
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        private static IHttpClientBuilder AddRetryPolicy(this IHttpClientBuilder builder, RefitApiSetting settings)
        {
            if (settings == null)
            {
                return builder;
            }

            if (settings.RetryCount > 0 && settings.RetryWaitMilliseconds > 0)
            {
                var hasTimeout = settings.TimeoutSeconds > 0;
                var retryCount = settings.RetryCount;
                var waitTime = settings.RetryWaitMilliseconds;

                var policyBuilder = HttpPolicyExtensions.HandleTransientHttpError();

                if (hasTimeout)
                {
                    policyBuilder.Or<TimeoutRejectedException>();
                }

                var policy = policyBuilder.WaitAndRetryAsync(retryCount, _ => TimeSpan.FromMilliseconds(waitTime));
                builder.AddPolicyHandler(policy);
            }

            return builder;
        }

        /// <summary>
        /// 設定 Request Timeout 原則
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        private static IHttpClientBuilder AddTimeoutPolicy(this IHttpClientBuilder builder, RefitApiSetting settings)
        {
            if (settings == null || settings.TimeoutSeconds <= 0)
            {
                return builder;
            }

            var policy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(settings.TimeoutSeconds));
            builder.AddPolicyHandler(policy);
            return builder;
        }
    }
}
