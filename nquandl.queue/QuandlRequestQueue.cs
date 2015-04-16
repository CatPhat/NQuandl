using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NQuandl.Client;


namespace NQuandl.Queue
{

    public interface IQuandlRequestQueue<T> where T : QuandlResponse
    {
        Task<IEnumerable<T>> ConsumeAsync(IEnumerable<BaseQuandlRequest<T>> queueRequest);

        Task<IEnumerable<T>> ConsumeAsync(IEnumerable<BaseQuandlRequest<T>> queueRequest,
            QueueStatusDelegate statusDelegate);
    }

    public class QuandlRequestQueue<T> : IQuandlRequestQueue<T> where T : QuandlResponse
    {
        private readonly IDownloadQueue _downloadQueue;
        private readonly BufferBlock<IEnumerable<BaseQuandlRequest<T>>> _inputBlock;
        private readonly TransformBlock<IEnumerable<BaseQuandlRequest<T>>, IEnumerable<string>> _getQueueResponseBlock;
        private readonly TransformBlock<IEnumerable<string>, IEnumerable<T>> _outputBlock;

        public QuandlRequestQueue(IDownloadQueue downloadQueue)
        {
            _downloadQueue = downloadQueue;
            var dataflowLinkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            _inputBlock = new BufferBlock<IEnumerable<BaseQuandlRequest<T>>>();
            _getQueueResponseBlock =
                new TransformBlock<IEnumerable<BaseQuandlRequest<T>>, IEnumerable<string>>(
                    async (x) => await _downloadQueue.ConsumeUrlStringsAsync(x.Select(y => y.Url)));

            _outputBlock = new TransformBlock<IEnumerable<string>, IEnumerable<T>>(async (x) =>
            {
                var deserializedObjectList = new List<T>();
                foreach (var stringResponse in x)
                {
                    deserializedObjectList.Add(await stringResponse.DeserializeToObjectAsync<T>());
                }
                return deserializedObjectList;
            });

            _inputBlock.LinkTo(_getQueueResponseBlock, dataflowLinkOptions);
            _getQueueResponseBlock.LinkTo(_outputBlock, dataflowLinkOptions);
        }

        public async Task<IEnumerable<T>> ConsumeAsync(IEnumerable<BaseQuandlRequest<T>> queueRequest)
        {
            var responseList = new List<T>();
            await SendAsync(queueRequest);
            while (await _inputBlock.OutputAvailableAsync())
            {
                await _inputBlock.ReceiveAsync();
            }

            while (await _outputBlock.OutputAvailableAsync())
            {
                responseList.AddRange(await _outputBlock.ReceiveAsync());
            }
            await _outputBlock.Completion;
            return responseList;
        }

        private async Task SendAsync(IEnumerable<BaseQuandlRequest<T>> requests)
        {
            await _inputBlock.SendAsync(requests);
            _inputBlock.Complete();
        }


        public async Task<IEnumerable<T>> ConsumeAsync(IEnumerable<BaseQuandlRequest<T>> queueRequest, QueueStatusDelegate statusDelegate)
        {
            var responseList = new List<T>();

            var queueRequestList = queueRequest.ToList();

            var dataflowLinkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            var actionBlock = new ActionBlock<IEnumerable<string>>(response => statusDelegate(response));
            var broadcastBlock = new BroadcastBlock<IEnumerable<string>>(x =>
            {
                var enumerable = x as IList<string> ?? x.ToList();
                return enumerable;
            });

            _inputBlock.LinkTo(_getQueueResponseBlock, dataflowLinkOptions);
            _getQueueResponseBlock.LinkTo(broadcastBlock, dataflowLinkOptions);

            broadcastBlock.LinkTo(_outputBlock, dataflowLinkOptions);
            broadcastBlock.LinkTo(actionBlock, dataflowLinkOptions);

            await SendAsync(queueRequestList);

            while (await _inputBlock.OutputAvailableAsync())
            {
                await _inputBlock.ReceiveAsync();
            }

            while (await _outputBlock.OutputAvailableAsync())
            {
                responseList.AddRange(await _outputBlock.ReceiveAsync());
            }

            return responseList;
        }

    }








}



















