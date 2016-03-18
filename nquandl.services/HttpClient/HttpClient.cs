using System;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Api.Configuration;
using NQuandl.Api.Quandl;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Services.HttpClient
{
    public class HttpClient : System.Net.Http.HttpClient, IHttpClient
    {
        private readonly AppConfiguration _configuration;

        public HttpClient(AppConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            _configuration = configuration;

            BaseAddress = new Uri(_configuration.BaseUrl);
        }

#pragma warning disable 108,114
        public async Task<HttpClientResponse> GetAsync(string requestUri)
#pragma warning restore 108,114
        {
            var result = await base.GetAsync(requestUri);

            var response = new HttpClientResponse
            {
                ContentStream = await result.Content.ReadAsStreamAsync(),
                IsStatusSuccessCode = result.IsSuccessStatusCode,
                StatusCode = result.StatusCode.ToString(),
                ResponseHeaders =
                    result.Headers.ToDictionary(httpHeader => httpHeader.Key, httpHeader => httpHeader.Value)
            };

            return response;
        }
    }
}