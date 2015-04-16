using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQuandl.Queue
{
    public class DownloadQueueDecorator : IDownloadQueue
    {
        private readonly IDownloadQueue _downloadQueue;
        private readonly IDownloadQueueLogger _logger;
        public DownloadQueueDecorator(IDownloadQueue downloadQueue, IDownloadQueueLogger logger)
        {
            _downloadQueue = downloadQueue;
            _logger = logger;
        }

        public async Task<IEnumerable<string>> ConsumeUrlStringsAsync(List<string> urls)
        {
            _logger.AddUnprocessedRequestsCount(urls);
            var responses = await _downloadQueue.ConsumeUrlStringsAsync(urls);
            _logger.AddRequestsProcessedCount(responses.ToList());
            return responses;
        }
    }
}
