using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Api.Quandl;
using NQuandl.Domain.Quandl.Responses;
using NQuandl.Services.Logger;

namespace NQuandl.Services.HttpClient
{
    public class HttpClientLoggerDecorator : IHttpClient
    {
        private readonly Func<IHttpClient> _httpFactory;
        private readonly ILogger _logger;

        public HttpClientLoggerDecorator([NotNull] Func<IHttpClient> httpFactory, ILogger logger)
        {
            if (httpFactory == null) throw new ArgumentNullException(nameof(httpFactory));

            _httpFactory = httpFactory;
            _logger = logger;
        }

        private int InboundRequests { get; set; }
        private int CompletedRequests { get; set; }

        public int RequestsRemaining
        {
            get { return InboundRequests - CompletedRequests; }
        }

        public async Task<HttpClientResponse> GetAsync(string requestUri)
        {
            var inboundEntry = new InboundRequestLogEntry {InboundRequestUri = requestUri, StartTime = DateTime.Now};
            await _logger.AddInboundRequest(inboundEntry);
            var result = await _httpFactory().GetAsync(requestUri);
            await _logger.AddCompletedRequest(new CompletedRequestLogEntry
            {
                InboundRequestLogEntry = inboundEntry,
            });
            return result;
        }
    }
}