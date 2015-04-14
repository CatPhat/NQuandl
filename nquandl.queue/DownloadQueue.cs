using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NQuandl.Client;

namespace NQuandl.Queue
{
    public interface IDownloadQueue
    {
        Task<string> AttachToBufferBlock(BufferBlock<string> bufferBlock);
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
                await Task.Delay(10);
                return await new WebClientHttpConsumer().DownloadStringAsync(x);
            }, new ExecutionDataflowBlockOptions{MaxDegreeOfParallelism = 1});
        }

        public async Task<string> AttachToBufferBlock(BufferBlock<string> bufferBlock)
        {
            bufferBlock.LinkTo(_urlBufferBlock);
            bufferBlock.Complete();
            _urlBufferBlock.LinkTo(_delayedDownloadBlock);
            return await _delayedDownloadBlock.ReceiveAsync();
        }
    }
}
