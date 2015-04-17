using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NQuandl.Queue
{
    public interface IDownloadQueueLogger
    {
        Task AddUnprocessedRequestCountAsync(int amount);
        Task AddProcessedRequestCountAsync(int amount);
        QueueStatus GetQueueStatus();
    }

    public class DownloadQueueLogger : IDownloadQueueLogger
    {
        private readonly Stopwatch _stopWatch;
        private readonly QueueStatus _queueStatus;
        public DownloadQueueLogger()
        {
            _stopWatch = new Stopwatch();
            _queueStatus = new QueueStatus();
            _stopWatch.Start();
        }

        public Task AddUnprocessedRequestCountAsync(int amount)
        {
            _queueStatus.TotalRequests = _queueStatus.TotalRequests + amount;
            return Task.FromResult(0);
        }


        public Task AddProcessedRequestCountAsync(int amount)
        {
            _queueStatus.RequestsProcessed = _queueStatus.RequestsProcessed + amount;
            return Task.FromResult(0);
        }

        public QueueStatus GetQueueStatus()
        {
            _queueStatus.TimeElapsed = _stopWatch.Elapsed;
            return _queueStatus;
        }
    }
}
