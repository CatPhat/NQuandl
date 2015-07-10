using System;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using NQuandl.Client.Api;
using NQuandl.Client.Domain.RequestParameters;

namespace NQuandl.Client.Domain
{
    /// <summary>
    ///     Class for Consuming Quandl REST API
    /// </summary>
    public class QuandlRestClient : IQuandlRestClient
    {
        private readonly string _baseUrl;
        private readonly IHttpClient _client;

        public QuandlRestClient(string baseUrl, IHttpClient client)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (string.IsNullOrEmpty(baseUrl)) throw new ArgumentException("baseUrl");
            _baseUrl = baseUrl;
            _client = client;
        }

        public async Task<string> DoGetRequestAsync(QuandlRestClientRequestParameters parameters)
        {
            string result;
            using (var client = _client.HttpClient)
            {
                var url = CreateUrl(parameters);
                result = await client.GetStringAsync(url);
            }

            return result;
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