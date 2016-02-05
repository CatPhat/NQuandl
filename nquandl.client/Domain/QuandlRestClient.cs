using System;
using System.Net.Http;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Api.Helpers;
using NQuandl.Client.Domain.RequestParameters;

namespace NQuandl.Client.Domain
{
    /// <summary>
    ///     Class for Consuming Quandl REST API
    /// </summary>
    public class QuandlRestClient : IQuandlRestClient
    {
        private readonly IHttpClient _client;
        private readonly string _apiKey;

        public QuandlRestClient(IHttpClient client, string apiKey = null)
        {
            if (client == null) throw new ArgumentNullException("client");
            _client = client;
            _apiKey = apiKey;
        }

        public async Task<HttpResponseMessage> GetFullResponseAsync(QuandlRestClientRequestParameters parameters)
        {
            try
            {
                return await _client.GetAsync(parameters.ToUri(_apiKey));
            }
            catch (HttpRequestException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
