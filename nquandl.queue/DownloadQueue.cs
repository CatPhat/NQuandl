using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NQuandl.Client;

namespace NQuandl.Queue
{
   

    public interface IDownloadQueue
    {
        Task<QueueResponse> ConsumeUrlStringAsync(string url);
    }

    public class DownloadQueue : IDownloadQueue
    {
        private readonly BufferBlock<string> _urlBufferBlock;
        private readonly TransformBlock<string, string> _delayedDownloadBlock;
        private readonly TransformBlock<string, QueueResponse> _outputBlock;
        private int _requestsProcessedCount;
       

        public DownloadQueue()
        {
            _requestsProcessedCount = 0;
            _urlBufferBlock = new BufferBlock<string>();
            _delayedDownloadBlock = new TransformBlock<string, string>(async (x) =>
            {
              
                await Task.Delay(300); // (10 minutes)/(2000 requests) = 300ms
                return await new WebClientHttpConsumer().DownloadStringAsync(x);

            }, new ExecutionDataflowBlockOptions{MaxDegreeOfParallelism = 1, MaxMessagesPerTask = 1, SingleProducerConstrained = true});
            _outputBlock = new TransformBlock<string, QueueResponse>(x =>
            {
                _requestsProcessedCount = _requestsProcessedCount + 1;
                var queueResponse = new QueueResponse();
                var queueStatus = new QueueStatus
                {
                    RequestsProcessed = _requestsProcessedCount,
                    RequestsRemaining = _urlBufferBlock.Count
                };
                queueResponse.QueueStatus = queueStatus;
                queueResponse.StringResponse = x;
                return queueResponse;
            });
        }

        public async Task<QueueResponse> ConsumeUrlStringAsync(string url)
        {
            var bufferBlock = new BufferBlock<string>();
            bufferBlock.LinkTo(_urlBufferBlock);
            await bufferBlock.SendAsync(url);
            bufferBlock.Complete();
            _urlBufferBlock.LinkTo(_delayedDownloadBlock);
            _delayedDownloadBlock.LinkTo(_outputBlock);
            return await _outputBlock.ReceiveAsync();
        }

       
        
    }
}
