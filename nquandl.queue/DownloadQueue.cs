using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NQuandl.Client;

namespace NQuandl.Queue
{
   

    public interface IDownloadQueue
    {
        Task<BufferBlock<QueueResponse>> ConsumeUrlStringsAsync(IEnumerable<string> url);
    }

    public class DownloadQueue : IDownloadQueue
    {
        private readonly BufferBlock<string> _urlBufferBlock;
        private readonly TransformBlock<string, string> _delayedDownloadBlock;
        private readonly TransformBlock<string, QueueResponse> _outputBlock;
        private int _requestsProcessedCount;
        private int _requestsRecieved;

        public DownloadQueue()
        {
            _requestsProcessedCount = 0;
            _urlBufferBlock = new BufferBlock<string>();
            _delayedDownloadBlock = new TransformBlock<string, string>(async (x) =>
            {
                await Task.Delay(300); // (10 minutes)/(2000 requests) = 300ms
                return await new WebClientHttpConsumer().DownloadStringAsync(x);

            }, new ExecutionDataflowBlockOptions{MaxDegreeOfParallelism = 1});
            _outputBlock = new TransformBlock<string, QueueResponse>(x =>
            {
                _requestsProcessedCount = _requestsProcessedCount + 1;
                var queueResponse = new QueueResponse();
                var queueStatus = new QueueStatus
                {
                    RequestsProcessed = _requestsProcessedCount,
                    RequestsRemaining = _requestsRecieved - _requestsProcessedCount
                };
                queueResponse.QueueStatus = queueStatus;
                queueResponse.StringResponse = x;
                return queueResponse;
            }, new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 1 });
        }

        public async Task<BufferBlock<QueueResponse>> ConsumeUrlStringsAsync(IEnumerable<string> urls)
        {
            var urlList = urls.ToList();
            _requestsRecieved = urlList.Count() + _requestsRecieved;

            var input = new BufferBlock<string>();
            var output = new BufferBlock<QueueResponse>();

            input.LinkTo(_urlBufferBlock);
            _outputBlock.LinkTo(output);

            foreach (var url in urlList)
            {
                input.Post(url);
            }
            
            input.Complete();
            _urlBufferBlock.LinkTo(_delayedDownloadBlock);
            _delayedDownloadBlock.LinkTo(_outputBlock);
            while (await input.OutputAvailableAsync())
            {
                await input.ReceiveAsync();
            }
            return output;
        }

       
        
    }
}
