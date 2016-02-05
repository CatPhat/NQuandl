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
    public class QuandlClient : IQuandlClient
    {
        private readonly IHttpClient _client;
        private readonly string _apiKey;

        public QuandlClient(IHttpClient client, string apiKey = null)
        {
            if (client == null) throw new ArgumentNullException("client");
            _client = client;
            _apiKey = apiKey;
        }

        public async Task<HttpResponseMessage> GetFullResponseAsync(QuandlClientRequestParameters parameters)
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
