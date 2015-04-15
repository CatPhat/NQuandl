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
        private readonly BufferBlock<BaseQuandlRequest<T>> _inputBlock;
        private readonly TransformBlock<BaseQuandlRequest<T>, QueueResponse> _getQueueResponseBlock;
        private readonly TransformBlock<QueueResponse, T> _transformToDeserializedObjectBlock;
      


        public QuandlRequestQueue(IDownloadQueue downloadQueue)
        {
            _downloadQueue = downloadQueue;

            _inputBlock = new BufferBlock<BaseQuandlRequest<T>>();
            _getQueueResponseBlock = new TransformBlock<BaseQuandlRequest<T>, QueueResponse>(async (x) => await _downloadQueue.ConsumeUrlStringAsync(x.Url));
            _transformToDeserializedObjectBlock = new TransformBlock<QueueResponse, T>(async (x) => await x.StringResponse.DeserializeToObjectAsync<T>());
        }

        public async Task<IEnumerable<T>> ConsumeAsync(IEnumerable<BaseQuandlRequest<T>> queueRequest)
        {
            var responseList = new List<T>();
            await Task.WhenAll(SendAsync(queueRequest), ProcessAsync());
            while (await _transformToDeserializedObjectBlock.OutputAvailableAsync())
            {
                responseList.Add(await _transformToDeserializedObjectBlock.ReceiveAsync());
            }
            return responseList;
        }

        public async Task<IEnumerable<T>> ConsumeAsync(IEnumerable<BaseQuandlRequest<T>> queueRequest, QueueStatusDelegate statusDelegate)
        {
            var responseList = new List<T>();

            var queueRequestList = queueRequest.ToList();
    
            var dataflowLinkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            var actionBlock = new ActionBlock<QueueResponse>(response => statusDelegate(response.QueueStatus));
            var broadcastBlock = new BroadcastBlock<QueueResponse>(x => x);

            _inputBlock.LinkTo(_getQueueResponseBlock, dataflowLinkOptions);
            _getQueueResponseBlock.LinkTo(broadcastBlock, dataflowLinkOptions);

            broadcastBlock.LinkTo(_transformToDeserializedObjectBlock, dataflowLinkOptions);
            broadcastBlock.LinkTo(actionBlock, dataflowLinkOptions);

            await SendAsync(queueRequestList);

            while (await _inputBlock.OutputAvailableAsync())
            {
                await _inputBlock.ReceiveAsync();
            }
            
            while (await _transformToDeserializedObjectBlock.OutputAvailableAsync())
            {
                responseList.Add(await _transformToDeserializedObjectBlock.ReceiveAsync());
            }

            return responseList;
        }

        private async Task SendAsync(IEnumerable<BaseQuandlRequest<T>> requests)
        {
            foreach (var request in requests)
            {
                await _inputBlock.SendAsync(request);
            }
            _inputBlock.Complete();
        }

        private async Task ProcessAsync()
        {
            var dataflowLinkOptions = new DataflowLinkOptions { PropagateCompletion = true };

            _inputBlock.LinkTo(_getQueueResponseBlock, dataflowLinkOptions);
            _getQueueResponseBlock.LinkTo(_transformToDeserializedObjectBlock, dataflowLinkOptions);

            while (await _inputBlock.OutputAvailableAsync())
            {
                await _inputBlock.ReceiveAsync();
            }
        }

    }








}



















