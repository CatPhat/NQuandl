using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NQuandl.Queue
{
    public interface IDownloadQueueLogger
    {
        void AddUnprocessedRequestsCount(List<string> requestsToProcess);
        void AddRequestsProcessedCount(List<string> requestsProcessed);
        QueueStatus GetQueueStatus();
    }

    public class DownloadQueueLogger : IDownloadQueueLogger
    {
        private readonly QueueStatus _queueStatus;
        public DownloadQueueLogger()
        {
            _queueStatus = new QueueStatus();
        }

        public void AddUnprocessedRequestsCount(List<string> requestsToProcess)
        {
            _queueStatus.TotalRequests = _queueStatus.TotalRequests + requestsToProcess.Count;

        }

        public void AddRequestsProcessedCount(List<string> requestsProcessed)
        {
            _queueStatus.RequestsProcessed = _queueStatus.RequestsProcessed + requestsProcessed.Count;
        }

        public QueueStatus GetQueueStatus()
        {
            return _queueStatus;
        }
    }
}
