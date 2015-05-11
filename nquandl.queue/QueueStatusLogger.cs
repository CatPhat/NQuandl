using System.Threading.Tasks;

namespace NQuandl.Queue
{
    public interface IQueueStatusLogger
    {
        Task AddUnprocessedCount(int amount);
        Task AddProcessedCount(int amount);
        QueueStatus Status { get; }
    }

    public class QueueStatusLogger : IQueueStatusLogger
    {
        private readonly QueueStatus _queueStatus;
        public QueueStatusLogger()
        {
            _queueStatus = new QueueStatus();
        }
        
        public Task AddUnprocessedCount(int amount)
        {
            _queueStatus.RequestsUnprocessed = _queueStatus.RequestsUnprocessed + amount;
            return Task.FromResult(0);
        }

        public Task AddProcessedCount(int amount)
        {
            _queueStatus.RequestsProcessed = _queueStatus.RequestsProcessed + amount;
            return Task.FromResult(0);
        }

        public QueueStatus Status
        {
            get { return _queueStatus; }
        }
    }
}