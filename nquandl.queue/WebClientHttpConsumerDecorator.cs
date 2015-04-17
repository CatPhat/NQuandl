using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NQuandl.Client;

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
            var response =  _client.DownloadStringAsync(url);
            return await response;
        }
    }
}
