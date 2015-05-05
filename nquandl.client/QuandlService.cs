using System;
using System.Net.Http;
using System.Threading.Tasks;
using NQuandl.Client.Requests;

namespace NQuandl.Client
{
    public class QuandlService
    {
        private readonly string _baseUrl;

        public QuandlService(string baseUrl)
        {
            _baseUrl = baseUrl;
        }
        
        public async Task<string> GetStringAsync(IQuandlRequest request)
        {
            var response = await GetAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> GetAsync(IQuandlRequest request)
        {
            var httpClient = new HttpClient();
            var uri = new Uri(_baseUrl + request.Uri);
            return await httpClient.GetAsync(uri);
        }
    }
}