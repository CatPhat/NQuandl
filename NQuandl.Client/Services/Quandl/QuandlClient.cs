using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using NQuandl.Client.Api.Quandl;
using NQuandl.Client.Api.Quandl.Helpers;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Services.Quandl
{
    /// <summary>
    ///     Class for Consuming Quandl REST API
    /// </summary>
    public class QuandlClient : IQuandlClient
    {
        private readonly IHttpClient _client;


        public QuandlClient(IHttpClient client)
        {
            if (client == null)
                throw new ArgumentNullException("client");

            _client = client;
        }

        public async Task<ResultStringWithQuandlResponseInfo> GetStringAsync(string uri)
        {
            var response = await GetHttpResponse(uri);

            if (!response.IsStatusSuccessCode)
                return new ResultStringWithQuandlResponseInfo
                {
                    QuandlClientResponseInfo = response.GetResponseInfo()
                };

            string contentString;
            using (var sr = new StreamReader(response.ContentStream))
            {
                contentString = await sr.ReadToEndAsync();
            }

            return new ResultStringWithQuandlResponseInfo
            {
                QuandlClientResponseInfo = response.GetResponseInfo(),
                ContentString = contentString
            };
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

            TResult result;
            if (response.IsStatusSuccessCode)
            {
                result = response.ContentStream.DeserializeToEntity<TResult>();
            }
            else
            {
                result = (TResult) Activator.CreateInstance(typeof (TResult));
            }


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