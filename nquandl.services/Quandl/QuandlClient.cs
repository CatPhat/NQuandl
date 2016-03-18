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
                QuandlClientResponseInfo = GetResponseInfo(response),
                ContentString = await response.Content.ReadAsStringAsync()
            };
        }

        public async Task<ResultStreamWithQuandlResponseInfo> GetStreamAsync(QuandlClientRequestParameters parameters)
        {
            var response = await GetHttpResponse(parameters);
            return new ResultStreamWithQuandlResponseInfo
            {
                QuandlClientResponseInfo = GetResponseInfo(response),
                ContentStream = await response.Content.ReadAsStreamAsync()
            };
        }

        public async Task<TResult> GetAsync<TResult>(
            QuandlClientRequestParameters parameters)
            where TResult : ResultWithQuandlResponseInfo
        {
            var response = await GetHttpResponse(parameters);

            var serializer = new JsonSerializer();
            using (var sr = new StreamReader(await response.Content.ReadAsStreamAsync()))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                var result = serializer.Deserialize<TResult>(jsonTextReader);
                result.QuandlClientResponseInfo = GetResponseInfo(response);
                return result;
            }
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

        private static QuandlClientResponseInfo GetResponseInfo(HttpResponseMessage response)
        {
            if (response.Headers == null)
            {
                return null;
            }
            var headers = response.Headers.ToDictionary(httpHeader => httpHeader.Key, httpHeader => httpHeader.Value);

            const string rateLimitKey = "X-RateLimit-Limit";
            const string rateLimitRemainingKey = "X-RateLimit-Remaining";

            int? rateLimit = null;
            if (headers.ContainsKey(rateLimitKey))
            {
                var rateLimitString = headers.FirstOrDefault(x => x.Key == rateLimitKey).Value.FirstOrDefault();
                int temp;
                int.TryParse(rateLimitString, out temp);
                rateLimit = temp;
            }

            int? rateLimitRemaining = null;
            if (headers.ContainsKey(rateLimitRemainingKey))
            {
                var rateLimitString = headers.FirstOrDefault(x => x.Key == rateLimitRemainingKey).Value.FirstOrDefault();
                int temp;
                int.TryParse(rateLimitString, out temp);
                rateLimitRemaining = temp;
            }


            return new QuandlClientResponseInfo
            {
                IsStatusSuccessCode = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode.ToString(),
                ResponseHeaders = headers,
                RateLimit = rateLimit,
                RateLimitRemaining = rateLimitRemaining
            };
        }
    }
}