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
        Task<IEnumerable<string>> ConsumeUrlStringsAsync(List<string> urls);
    }

    public class DownloadQueue : IDownloadQueue
    {
        private readonly IConsumeHttp _client;
        private readonly BufferBlock<IEnumerable<string>> _inputBlock;
        private readonly TransformBlock<IEnumerable<string>, IEnumerable<string>> _outputBlock;

        public DownloadQueue(IConsumeHttp client)
        {
            _client = client;
            _inputBlock = new BufferBlock<IEnumerable<string>>();
            _outputBlock = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(async (urls) =>
            {
                var urlList = new List<string>();
                foreach (var url in urls)
                {
                    await Task.Delay(300); // (10 minutes)/(2000 requests) = 300ms
                    urlList.Add(await _client.DownloadStringAsync(url));
                }
                return urlList;
            }, new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 1 });
        }

        public async Task<IEnumerable<string>> ConsumeUrlStringsAsync(List<string> urls)
        {
            var bufferBlock = new BufferBlock<IEnumerable<string>>();
            bufferBlock.LinkTo(_inputBlock);
            bufferBlock.Post(urls.ToList());
            bufferBlock.Complete();
            _inputBlock.LinkTo(_outputBlock);
            return await _outputBlock.ReceiveAsync();
        }
       
    }
}
