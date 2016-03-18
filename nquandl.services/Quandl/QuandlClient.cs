using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
            return new ResultStringWithQuandlResponseInfo
            {
                QuandlClientResponseInfo = response.GetResponseInfo(),
                ContentString = await response.Content.ReadAsStringAsync()
            };
        }

        public async Task<ResultStreamWithQuandlResponseInfo> GetStreamAsync(QuandlClientRequestParameters parameters)
        {
            var response = await GetHttpResponse(parameters);
            return new ResultStreamWithQuandlResponseInfo
            {
                QuandlClientResponseInfo =  response.GetResponseInfo(),
                ContentStream = await response.Content.ReadAsStreamAsync()
            };
        }

        public async Task<TResult> GetAsync<TResult>(
            QuandlClientRequestParameters parameters)
            where TResult : ResultWithQuandlResponseInfo
        {
            var response = await GetHttpResponse(parameters);

            var stream = await response.Content.ReadAsStreamAsync();
            var result = stream.DeserializeToEntity<TResult>();
            result.QuandlClientResponseInfo = response.GetResponseInfo();
            return result;

        }

        private async Task<HttpResponseMessage> GetHttpResponse(QuandlClientRequestParameters parameters)
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