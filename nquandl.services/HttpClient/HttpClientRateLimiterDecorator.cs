using System;
using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Api.Quandl;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Services.HttpClient
{
    public class HttpClientRateLimiterDecorator : IHttpClient
    {
        private readonly Func<IHttpClient> _httpClientFactory;
        private readonly IRateGate _rateGate;

        public HttpClientRateLimiterDecorator([NotNull] Func<IHttpClient> httpClientFactory,
            [NotNull] IRateGate rateGate)
        {
            if (httpClientFactory == null) throw new ArgumentNullException(nameof(httpClientFactory));
            if (rateGate == null) throw new ArgumentNullException(nameof(rateGate));
            _httpClientFactory = httpClientFactory;
            _rateGate = rateGate;
        }

        public Uri BaseAddress { get; set; }
        public TimeSpan Timeout { get; set; }

        public async Task<HttpClientResponse> GetAsync(string requestUri)
        {
            await _rateGate.WaitToProceedAsync();
            return await _httpClientFactory().GetAsync(requestUri);
        }
    }
}