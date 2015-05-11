using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NQuandl.Client.Entities.Base;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Requests;
using NQuandl.Client.Responses;

namespace NQuandl.Queue
{
    public interface IJsonServiceQueue
    {
        Task<DeserializedEntityResponse<TEntity>> GetAsync<TEntity>(
            RequestParameterOptions options = null)
            where TEntity : QuandlEntity, new();

        Task<IEnumerable<DeserializedEntityResponse<TEntity>>> GetAsync<TEntity>(
            List<QueueRequest<TEntity>> requests) where TEntity : QuandlEntity, new();

        TransformBlock<IQuandlRequest, string> Queue { get; }
    }

    public class JsonServiceQueue : IJsonServiceQueue
    {
        private readonly IQuandlService _quandl;
        private readonly TransformBlock<IQuandlRequest, string> _queue;

        public JsonServiceQueue(IQuandlService quandl)
        {
            _quandl = quandl;
            _queue = new TransformBlock<IQuandlRequest, string>(async x =>
            {
                await Task.Delay(300); // (10 minutes)/(2000 requests) = 300ms
                return await _quandl.GetStringAsync(x);
            }, new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = 1
            });
        }

        public async Task<DeserializedEntityResponse<TEntity>> GetAsync<TEntity>(
         RequestParameterOptions options = null)
         where TEntity : QuandlEntity, new()
        {
            var request = new DeserializeEntityRequest<TEntity> { Options = options };
            return await GetAsync(request);
        }

        public async Task<DeserializedEntityResponse<TEntity>> GetAsync<TEntity>(
            IDeserializedEntityRequest<TEntity> request)
            where TEntity : QuandlEntity
        {
            var inputQueue = new BufferBlock<IQuandlRequest>();
            inputQueue.LinkTo(_queue);
            await inputQueue.SendAsync(request);
            inputQueue.Complete();
            return new DeserializedEntityResponse<TEntity>(await _queue.ReceiveAsync(), request.Mapper);
        }

        public async Task<IEnumerable<DeserializedEntityResponse<TEntity>>> GetAsync<TEntity>(
            List<QueueRequest<TEntity>> requests) where TEntity : QuandlEntity, new()
        {
            var responses = new List<DeserializedEntityResponse<TEntity>>();
            var mapper = requests.First().DeserializeEntityRequest.Mapper;
            var inputBlock = new BufferBlock<IQuandlRequest>();
            inputBlock.LinkTo(_queue);
            foreach (var queueRequest in requests)
            {
               await inputBlock.SendAsync(queueRequest.DeserializeEntityRequest);
            }
            inputBlock.Complete();
            while (await _queue.OutputAvailableAsync())
            {
                responses.Add(new DeserializedEntityResponse<TEntity>(await _queue.ReceiveAsync(),mapper));
            }
            return responses;
        }

        public TransformBlock<IQuandlRequest, string> Queue
        {
            get { return _queue; }
        }
    }
}