using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace RefitWebApiClient.Extensions
{
    public static class RefitHttpApiClientExtension
    {
        /// <summary>
        /// 加入 Refit API Service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static IServiceCollection AddRefitApiClient(this IServiceCollection services,
                                                           RefitApiSetting settings)
        {
            // 加入 Refit Rest Service Factory
            services.AddTransient<IRefiRestServiceFactory, RefitRestServiceFactory>();

            // 加入 Http Clinet
            var builder = services.AddHttpClient(IRefiRestServiceFactory.ClientName, (httpClient) =>
            {
                httpClient.BaseAddress = new Uri(settings.HostUrl);
            });

            // 忽略 Https 憑證檢查
            if (!settings.CertificateValidation)
            {
                builder.ConfigurePrimaryHttpMessageHandler(_ =>
                {
                    var handler = new HttpClientHandler()
                    {
                        ClientCertificateOptions = ClientCertificateOption.Manual,
                        ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
                    };
                    return handler;
                });
            }

            builder.AddRetryPolicy(settings)
                   .AddTimeoutPolicy(settings);
            return services;
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
        /// 設定 Timeout 原則
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
