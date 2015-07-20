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

        public QuandlRestClient(IHttpClient client)
        {
            if (client == null) throw new ArgumentNullException("client");
            _client = client;
        }

        public async Task<string> GetStringAsync(QuandlRestClientRequestParameters parameters)
        {
            try
            {
                var fullResponse = await _client.GetAsync(parameters.ToUri());
                fullResponse.EnsureSuccessStatusCode();
                var response = await fullResponse.Content.ReadAsStringAsync();
                return response;
            }
            catch (HttpRequestException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}