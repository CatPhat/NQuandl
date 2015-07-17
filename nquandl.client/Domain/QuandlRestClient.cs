using System;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
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

        public async Task<string> DoGetRequestAsync(QuandlRestClientRequestParameters parameters)
        {
            var url = parameters.GetUrl(_baseUrl);
            string result;
            using (var client = new HttpClient())
            {
                result = await client.GetStringAsync(url); 
            }
            return result;
        }

      
    }
}