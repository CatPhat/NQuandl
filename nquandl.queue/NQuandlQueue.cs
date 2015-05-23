using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NQuandl.Client.Entities.Base;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Requests;
using NQuandl.Client.Responses;

namespace NQuandl.Queue
{
    public class NQuandlQueue : INQuandlQueue
    {
        private readonly BroadcastBlock<string> _broadcastBlock;
        private readonly TransformBlock<IQuandlRequest, string> _client;
        private readonly BufferBlock<string> _outputBlock;
        private readonly IQuandlService _quandl;
        private readonly TransformBlock<IQuandlRequest, IQuandlRequest> _queue;

        public NQuandlQueue(IQuandlService quandl)
        {
            _quandl = quandl;
            _queue = new TransformBlock<IQuandlRequest, IQuandlRequest>(async x =>
            {
                await Task.Delay(300); // (10 minutes)/(2000 requests) = 300ms);
                return x;
            });
            _client = new TransformBlock<IQuandlRequest, string>(async x => await _quandl.GetStringAsync(x),
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = -1
                });
            _broadcastBlock = new BroadcastBlock<string>(x => x);
            _outputBlock = new BufferBlock<string>();
            _queue.LinkTo(_client);
            _client.LinkTo(_broadcastBlock);
            _broadcastBlock.LinkTo(_outputBlock);
        }

        public TransformBlock<IQuandlRequest, IQuandlRequest> Queue
        {
            get { return _queue; }
        }

        public BroadcastBlock<string> BroadcastBlock
        {
            get { return _broadcastBlock; }
        }

        public async Task<string> GetStringAsync(IQuandlRequest request)
        {
            var inputQueue = new BufferBlock<IQuandlRequest>();
            inputQueue.LinkTo(_queue);
            await inputQueue.SendAsync(request);
            inputQueue.Complete();
            var response = await _outputBlock.ReceiveAsync();
            return response;
        }

        public async Task<IEnumerable<string>> GetStringsAsync(IEnumerable<IQuandlRequest> requests)
        {
            var responses = new List<string>();
            var inputBlock = new BufferBlock<IQuandlRequest>();
            inputBlock.LinkTo(_queue);
            foreach (var queueRequest in requests)
            {
                await inputBlock.SendAsync(queueRequest);
            }
            inputBlock.Complete();
            while (await _outputBlock.OutputAvailableAsync())
            {
                responses.Add(await _outputBlock.ReceiveAsync());
            }
            return responses;
        }

        public async Task<DeserializedEntityResponse<TEntity>> GetAsync<TEntity>(
            RequestOptionsV1 options = null)
            where TEntity : QuandlEntity, new()
        {
            var request = new DeserializeEntityRequestV1<TEntity> {Options = options};
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
            return new DeserializedEntityResponse<TEntity>(await _outputBlock.ReceiveAsync(), request.Mapper);
        }

        public async Task<IEnumerable<DeserializedEntityResponse<TEntity>>> GetAsync<TEntity>(
            List<QueueRequest<TEntity>> requests) where TEntity : QuandlEntity, new()
        {
            var responses = new List<DeserializedEntityResponse<TEntity>>();
            var mapper = requests.First().DeserializeEntityRequestV1.Mapper;
            var inputBlock = new BufferBlock<IQuandlRequest>();
            inputBlock.LinkTo(_queue);
            foreach (var queueRequest in requests)
            {
                await inputBlock.SendAsync(queueRequest.DeserializeEntityRequestV1);
            }
            inputBlock.Complete();
            while (await _outputBlock.OutputAvailableAsync())
            {
                responses.Add(new DeserializedEntityResponse<TEntity>(await _outputBlock.ReceiveAsync(), mapper));
            }
            return responses;
        }
    }
}