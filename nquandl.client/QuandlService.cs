using System.Threading.Tasks;
using NQuandl.Client.Helpers;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Responses;

namespace NQuandl.Client
{
    public class QuandlService
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public QuandlService(string baseUrl, string apiKey)
        {
            _baseUrl = baseUrl;
            _apiKey = apiKey;
        }

        public async Task<TResponse> GetAsync<TResponse>(IReturn<TResponse> request) where TResponse : QuandlResponse
        {
            var response = await GetStringAsync(request);
            return await response.DeserializeToObjectAsync<TResponse>();
        }

        public async Task<string> GetStringAsync(IReturn request)
        {
            var url = new Url
            {
                ApiKey = _apiKey,
                BaseUrl = _baseUrl,
                Uri = request.Uri
            };
            return await GetStringAsync(url);
        }

        private static async Task<string> GetStringAsync(Url url)
        {
            return await new WebClientHttpConsumer().DownloadStringAsync(url.ToUrlString());
        }
    }
}