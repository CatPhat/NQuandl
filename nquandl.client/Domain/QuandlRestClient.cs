using System;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using NQuandl.Client.Api;
using NQuandl.Client.Domain.RequestParameters;

namespace NQuandl.Client.Domain
{
    /// <summary>
    ///     Class for Consuming Quandl REST API
    /// </summary>
    public class QuandlRestClient : IQuandlRestClient, IDisposable
    {
        private readonly string _baseUrl;
        private readonly IHttpClient _client;

        public QuandlRestClient(string baseUrl)
        {
            if (string.IsNullOrEmpty(baseUrl)) throw new ArgumentException("baseUrl");
            _baseUrl = baseUrl;
            _client = new HttpClient();
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public async Task<string> DoGetRequestAsync(QuandlRestClientRequestParameters parameters)
        {
            var url = CreateUrl(parameters);
            return await _client.HttpClient.GetStringAsync(url);
        }

        private string CreateUrl(QuandlRestClientRequestParameters parameters)
        {
            if (string.IsNullOrEmpty(parameters.PathSegment)) throw new ArgumentException("Missing PathSegment");

            var baseUrl = new Url(_baseUrl);

            var url = baseUrl.AppendPathSegment(parameters.PathSegment);
            if (parameters.QueryParameters.Any())
            {
                url = url.SetQueryParams(parameters.QueryParameters);
            }

            return url;
        }
    }
}