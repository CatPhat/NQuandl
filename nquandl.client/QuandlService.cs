using System.Threading.Tasks;
using NQuandl.Client.Helpers;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Responses;

namespace NQuandl.Client
{
    public class QuandlService
    {
        public async Task<TResponse> GetAsync<TResponse>(IReturn<TResponse> request) where TResponse : QuandlResponse
        {
            var response = await GetStringAsync(request.Url);
            return await response.DeserializeToObjectAsync<TResponse>();
        }

        public async Task<string> GetStringAsync(string url)
        {
            return await new WebClientHttpConsumer().DownloadStringAsync(url);
        }
    }
}