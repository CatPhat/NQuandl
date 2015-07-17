using System;
using System.Threading.Tasks;
using Flurl.Http;
using NQuandl.Client.Api;

namespace NQuandl.Client.Domain
{
    public class HttpClient : IHttpClient, IDisposable
    {
        private readonly FlurlClient _client;

        public HttpClient()
        {
            _client = new FlurlClient();
        }
        public async Task<string> GetStringAsync(string url)
        {
            return await _client.HttpClient.GetStringAsync(url);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}