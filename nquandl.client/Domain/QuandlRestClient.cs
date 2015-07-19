using System;
using System.Net.Http;
using System.Net.Http.Headers;
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
        private readonly string _baseUrl;

        public QuandlRestClient(string baseUrl)
        {
            if (string.IsNullOrEmpty(baseUrl)) throw new ArgumentException("baseUrl");
            _baseUrl = baseUrl;
        }

        public async Task<string> GetStringAsync(QuandlRestClientRequestParameters parameters)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var fullResponse = await client.GetAsync(parameters.ToUri());
                    fullResponse.EnsureSuccessStatusCode();
                    var response = await fullResponse.Content.ReadAsStringAsync();
                    return response;
                }
                catch(HttpRequestException e)
                {
                    throw new Exception(e.Message);
                }
            }
       
        }
    }
}