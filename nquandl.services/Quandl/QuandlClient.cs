using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using NQuandl.Api.Quandl;
using NQuandl.Api.Quandl.Helpers;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Services.Quandl
{
    /// <summary>
    ///     Class for Consuming Quandl REST API
    /// </summary>
    public class QuandlClient : IQuandlClient
    {
        private readonly IHttpClient _client;


        public QuandlClient(IHttpClient client)
        {
            if (client == null) throw new ArgumentNullException("client");

            _client = client;
        }

        public async Task<ResultStringWithQuandlResponseInfo> GetStringAsync(string uri)
        {
            var response = await GetHttpResponse(uri);
            using (var sr = new StreamReader(response.ContentStream))
            {
                return new ResultStringWithQuandlResponseInfo
                {
                    QuandlClientResponseInfo = response.GetResponseInfo(),
                    ContentString = await sr.ReadToEndAsync()
                };
            }
        }

        public async Task<ResultStreamWithQuandlResponseInfo> GetStreamAsync(string uri)
        {
            var response = await GetHttpResponse(uri);
            return new ResultStreamWithQuandlResponseInfo
            {
                QuandlClientResponseInfo = response.GetResponseInfo(),
                ContentStream = response.ContentStream
            };
        }

        public async Task<TResult> GetAsync<TResult>(
            string uri)
            where TResult : ResultWithQuandlResponseInfo
        {
            var response = await GetHttpResponse(uri);
            var result = !response.IsStatusSuccessCode
                ? Activator.CreateInstance<TResult>()
                : response.ContentStream.DeserializeToEntity<TResult>();


            result.QuandlClientResponseInfo = response.GetResponseInfo();
            return result;
        }

        private async Task<HttpClientResponse> GetHttpResponse(string uri)
        {
            try
            {
                return await _client.GetAsync(uri);
            }
            catch (HttpRequestException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}