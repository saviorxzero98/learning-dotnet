using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace RefitWebApiClient.HeaderHandlers
{
    public class GenericApiHeaderHandler : DelegatingHandler
    {
        private readonly IContextGetter<GenericApiHeaderContext> _contextGetter;
        private readonly IConfiguration _configuration;

        public GenericApiHeaderHandler(IContextGetter<GenericApiHeaderContext> contextGetter,
                                       IConfiguration configuration)
        {
            _contextGetter = contextGetter;
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                     CancellationToken cancellationToken)
        {
            var headerContext = _contextGetter.GetValue();

            if (!string.IsNullOrEmpty(headerContext?.Authorization))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(headerContext.AuthorizationScheme,
                                                                              headerContext.Authorization);
            }
            
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
