using System;
using System.Collections.Generic;
using System.Linq;
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
        private int InboundRequests { get; set; }
        private int CompletedRequests { get; set; }

        public int RequestsRemaining
        {
            get { return InboundRequests - CompletedRequests; }
        }
        public HttpClientLoggerDecorator([NotNull] Func<IHttpClient> httpFactory, [NotNull] ILogger logger)
        {
            if (httpFactory == null) throw new ArgumentNullException(nameof(httpFactory));
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            _httpFactory = httpFactory;
            _logger = logger;
        }

        public async Task<HttpClientResponse> GetAsync(string requestUri)
        {
           
            _logger.Write("+1 reguest");
            var result = await _httpFactory().GetAsync(requestUri);
            _logger.Write("completed.");
            return result;
        }
    }
}
