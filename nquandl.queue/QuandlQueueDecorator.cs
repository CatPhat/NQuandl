using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NQuandl.Client.Api;
using NQuandl.Client._OLD.Requests;

namespace NQuandl.Queue
{
    public class QuandlQueueDecorator : INQuandlQueue
    {
        private readonly IQueueStatusLogger _logger;
        private readonly INQuandlQueue _queue;

     
        private readonly ActionBlock<string> _actionBlock;

        public QuandlQueueDecorator(INQuandlQueue queue, IQueueStatusLogger logger)
        {
            _queue = queue;
            _logger = logger;
            _actionBlock = new ActionBlock<string>(async (x) => await _logger.AddProcessedCount(1));
            BroadcastBlock.LinkTo(_actionBlock);
        }


        public BroadcastBlock<string> BroadcastBlock
        {
            get { return _queue.BroadcastBlock; }
        }

        public async Task<string> GetStringAsync(IQuandlRequest request)
        {
            await _logger.AddUnprocessedCount(1);
            var response = await _queue.GetStringAsync(request);
            _logger.Status.LastResponse = response;
            return response;
        }

        public async Task<IEnumerable<string>> GetStringsAsync(IEnumerable<IQuandlRequest> requests)
        {
            var quandlRequests = requests as IList<IQuandlRequest> ?? requests.ToList();
            await _logger.AddUnprocessedCount(quandlRequests.Count());
            var responses = await _queue.GetStringsAsync(quandlRequests);
            return responses;
        }

        public async Task<DeserializedEntityResponse<TEntity>> GetAsync<TEntity>(QueryParametersV1 options = null)
            where TEntity : QuandlEntity, new()
        {
            await _logger.AddUnprocessedCount(1);
            var response = await _queue.GetAsync<TEntity>(options);
            return response;
        }

        public Task<DeserializedEntityResponse<TEntity>> GetAsync<TEntity>(IDeserializedEntityRequest<TEntity> request) where TEntity : QuandlEntity
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<DeserializedEntityResponse<TEntity>>> GetAsync<TEntity>(
            List<QueueRequest<TEntity>> requests) where TEntity : QuandlEntity, new()
        {
            await _logger.AddUnprocessedCount(requests.Count);
            var responses = await _queue.GetAsync(requests);
            return responses;
        }

        public TransformBlock<IQuandlRequest, IQuandlRequest> Queue
        {
            get { return _queue.Queue; }
        }
    }
}