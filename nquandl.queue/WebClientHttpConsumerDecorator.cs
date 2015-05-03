using System.Threading.Tasks;
using NQuandl.Client.Interfaces;

namespace NQuandl.Queue
{
    public class WebClientHttpConsumerDecorator : IConsumeHttp
    {
        private readonly IConsumeHttp _client;
        private readonly IDownloadQueueLogger _logger;

        public WebClientHttpConsumerDecorator(IConsumeHttp client, IDownloadQueueLogger logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<string> DownloadStringAsync(string url, int? timeout = null, int retries = 0)
        {
            await _logger.AddProcessedRequestCountAsync(1);
            Task<string> response = _client.DownloadStringAsync(url);
            await _logger.SetLastResponseAsync(await response);
            return await response;
        }
    }
}