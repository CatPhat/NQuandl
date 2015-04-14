using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NQuandl.Client;

namespace NQuandl.Queue
{
    public interface IDownloadQueue
    {
        Task<string> ConsumeUrlStringAsync(string url);
    }

    public class DownloadQueue : IDownloadQueue
    {
        private readonly BufferBlock<string> _urlBufferBlock;
        private readonly TransformBlock<string, string> _delayedDownloadBlock;

        public DownloadQueue()
        {
            _urlBufferBlock = new BufferBlock<string>();
            _delayedDownloadBlock = new TransformBlock<string, string>(async (x) =>
            {
                await Task.Delay(300); // (10 minutes)/(2000 requests) = 300ms
                return await new WebClientHttpConsumer().DownloadStringAsync(x);
            }, new ExecutionDataflowBlockOptions{MaxDegreeOfParallelism = 1});
        }

        public async Task<string> ConsumeUrlStringAsync(string url)
        {
            var bufferBlock = new BufferBlock<string>();
            bufferBlock.LinkTo(_urlBufferBlock);
            bufferBlock.Post(url);
            bufferBlock.Complete();
            _urlBufferBlock.LinkTo(_delayedDownloadBlock);
            return await _delayedDownloadBlock.ReceiveAsync();
        }
    }
}
