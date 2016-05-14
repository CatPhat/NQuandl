using System;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Client.Api.Quandl;
using NQuandl.Client.Domain.Responses;
using NQuandl.Client.Services.Configuration;

namespace NQuandl.Client.Services.HttpClient
{
    public class HttpClient : System.Net.Http.HttpClient, IHttpClient
    {
        public HttpClient(IHttpClientConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            BaseAddress = new Uri(configuration.BaseUrl);
        }


        public new async Task<HttpClientResponse> GetAsync(string requestUri)
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