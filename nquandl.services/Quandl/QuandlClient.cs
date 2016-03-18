using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
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

        public async Task<ResultStringWithQuandlResponseInfo> GetStringAsync(QuandlClientRequestParameters parameters)
        {
            var response = await GetHttpResponse(parameters);
            using (var sr = new StreamReader(response.ContentStream))
            {
                return new ResultStringWithQuandlResponseInfo
                {
                    QuandlClientResponseInfo = response.GetResponseInfo(),
                    ContentString = await sr.ReadToEndAsync()
                };
            }
          
        }

        public async Task<ResultStreamWithQuandlResponseInfo> GetStreamAsync(QuandlClientRequestParameters parameters)
        {
            var response = await GetHttpResponse(parameters);
            return new ResultStreamWithQuandlResponseInfo
            {
                QuandlClientResponseInfo = response.GetResponseInfo(),
                ContentStream = response.ContentStream
            };
        }

        public async Task<TResult> GetAsync<TResult>(
            QuandlClientRequestParameters parameters)
            where TResult : ResultWithQuandlResponseInfo
        {
            var response = await GetHttpResponse(parameters);
            
            var result = response.ContentStream.DeserializeToEntity<TResult>();
            result.QuandlClientResponseInfo = response.GetResponseInfo();
            return result;
        }

        private async Task<HttpClientResponse> GetHttpResponse(QuandlClientRequestParameters parameters)
        {
            try
            {
                return await _client.GetAsync(parameters.ToUri(_configuration.ApiKey)).ConfigureAwait(false);
            }
            catch (HttpRequestException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}