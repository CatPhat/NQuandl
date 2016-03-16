using System;
using System.Net.Http;
using System.Threading.Tasks;
using NQuandl.Api;
using NQuandl.Api.Configuration;
using NQuandl.Api.Quandl;
using NQuandl.Api.Quandl.Helpers;
using NQuandl.Domain.Quandl.RequestParameters;
using NQuandl.Domain.Quandl.Responses;

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

        public async Task<RawHttpContent> GetFullResponseAsync(QuandlClientRequestParameters parameters)
        {
            try
            {
                var response = await _client.GetAsync(parameters.ToUri(_configuration.ApiKey));
                return new RawHttpContent
                {
                    Content = await response.Content.ReadAsStreamAsync(),
                    IsStatusSuccessCode = response.IsSuccessStatusCode,
                    StatusCode = response.StatusCode.ToString()
                };
            }
            catch (HttpRequestException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
