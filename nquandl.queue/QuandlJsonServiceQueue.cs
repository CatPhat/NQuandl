using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NQuandl.Client.Entities.Base;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Responses;

namespace NQuandl.Queue
{
    public class QuandlJsonServiceQueue
    {
        private readonly IQuandlService _quandl;
        private readonly TransformBlock<IQuandlRequest, string> _queue;

        public QuandlJsonServiceQueue(IQuandlService quandl)
        {
            _quandl = quandl;
            _queue = new TransformBlock<IQuandlRequest, string>(async x =>
            {
                await Task.Delay(300); // (10 minutes)/(2000 requests) = 300ms
                return await _quandl.GetStringAsync(x);
            }, new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = 1,
            });
        }

        public async Task<DeserializedEntityResponse<TEntity>> GetAsync<TEntity>(
            IDeserializedEntityRequest<TEntity> request)
            where TEntity : QuandlEntity
        {
            var inputQueue = new BufferBlock<IQuandlRequest>();
            await inputQueue.SendAsync(request);
            inputQueue.Complete();
            var stringResponse = await _queue.ReceiveAsync();
            return new DeserializedEntityResponse<TEntity>(stringResponse, request.Mapper);
        }
    }
}