using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Client.Api.Quandl;
using NQuandl.Client.Domain.Responses;
using NQuandl.Client.Services.Logger;

namespace NQuandl.Client.Services.HttpClient
{
    public class HttpClientLoggerDecorator : IHttpClient
    {
        private readonly Func<IHttpClient> _httpFactory;
        private readonly ILogger _logger;

        public HttpClientLoggerDecorator([NotNull] Func<IHttpClient> httpFactory, ILogger logger)
        {
            if (httpFactory == null)
                throw new ArgumentNullException(nameof(httpFactory));

            _httpFactory = httpFactory;
            _logger = logger;
        }

        public async Task<HttpClientResponse> GetAsync(string requestUri)
        {
           
            var inboundEntry = new InboundRequestLogEntry {InboundRequestUri = requestUri, StartTime = DateTime.Now};
            await _logger.AddInboundRequest(inboundEntry);
            var result = await _httpFactory().GetAsync(requestUri);

            await _logger.AddCompletedRequest(new CompletedRequestLogEntry
            {
                CompletedRequestUri = requestUri,
                StartTime = inboundEntry.StartTime,
                EndTime = DateTime.Now
            });
            return result;
        }
    }
}