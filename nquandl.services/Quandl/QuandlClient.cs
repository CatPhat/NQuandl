using System;
using System.Net.Http;
using System.Threading.Tasks;
using NQuandl.Api;
using NQuandl.Api.Configuration;
using NQuandl.Api.Helpers;
using NQuandl.Domain.RequestParameters;

namespace NQuandl.Services.Quandl
{
    /// <summary>
    ///     Class for Consuming Quandl REST API
    /// </summary>
    public class QuandlClient : IQuandlClient
    {
        private readonly IHttpClient _client;
        private readonly AppConfiguration _configuration;
     

        public QuandlClient(IHttpClient client, AppConfiguration configuration)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            _client = client;
            _configuration = configuration;
           
        }

        public async Task<HttpResponseMessage> GetFullResponseAsync(QuandlClientRequestParameters parameters)
        {
            try
            {
                return await _client.GetAsync(parameters.ToUri(_configuration.ApiKey));
            }
            catch (HttpRequestException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
