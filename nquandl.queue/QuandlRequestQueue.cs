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
        Task<IEnumerable<QueueResponse<T>>> ConsumeAsync(IEnumerable<BaseQuandlRequest<T>> queueRequest);

        Task<IEnumerable<QueueResponse<T>>> ConsumeAsync(IEnumerable<BaseQuandlRequest<T>> queueRequest,
            QueueStatusDelegate statusDelegate);
    }

    public class QuandlRequestQueue<T> : IQuandlRequestQueue<T> where T : QuandlResponse
    {
        private readonly IDownloadQueue _downloadQueue;
        private readonly BufferBlock<IEnumerable<BaseQuandlRequest<T>>> _inputBlock;
        private readonly TransformBlock<IEnumerable<BaseQuandlRequest<T>>, BufferBlock<QueueResponse>> _getQueueResponseBlock;
        private readonly ActionBlock<BufferBlock<QueueResponse>> _transformToDeserializedObjectBlock;
        private readonly BufferBlock<QueueResponse<T>> _outputBlock;

        public QuandlRequestQueue(IDownloadQueue downloadQueue)
        {
            _downloadQueue = downloadQueue;
            _inputBlock = new BufferBlock<IEnumerable<BaseQuandlRequest<T>>>();
            _outputBlock = new BufferBlock<QueueResponse<T>>();

            _getQueueResponseBlock = new TransformBlock<IEnumerable<BaseQuandlRequest<T>>, BufferBlock<QueueResponse>>((x) =>
            {
                var urlList = x.Select(request => request.Url).ToList();
                return _downloadQueue.ConsumeUrlStringsAsync(urlList);
            });

            _transformToDeserializedObjectBlock = new ActionBlock<BufferBlock<QueueResponse>>(async (x) =>
            {
                while (await x.OutputAvailableAsync())
                {
                    var task = await x.ReceiveAsync();
                    var stringResponse = task.StringResponse;
                    var stringResponseTask = stringResponse.DeserializeToObject<T>();
                    var queueResponse = new QueueResponse<T>
                    {
                        QuandlResponse = stringResponseTask,
                        QueueStatus = task.QueueStatus,
                        StringResponse = task.StringResponse
                    };
                    await _outputBlock.SendAsync(queueResponse);
                }
            });
        }

        public async Task<IEnumerable<QueueResponse<T>>> ConsumeAsync(IEnumerable<BaseQuandlRequest<T>> queueRequest)
        {
            var responseList = new List<QueueResponse<T>>();
            await Task.WhenAll(SendAsync(queueRequest), ProcessAsync());
            while (await _outputBlock.OutputAvailableAsync())
            {
                responseList.Add(await _outputBlock.ReceiveAsync());
            }
            return responseList;
        }

        public async Task<IEnumerable<QueueResponse<T>>> ConsumeAsync(IEnumerable<BaseQuandlRequest<T>> queueRequest, QueueStatusDelegate statusDelegate)
        {
            var responseList = new List<QueueResponse<T>>();

            var queueRequestList = queueRequest.ToList();

            
            var actionBlock = new ActionBlock<BufferBlock<QueueResponse>>(async (response) =>
            {
                while (await response.OutputAvailableAsync())
                {
                    var availableResponse = await response.ReceiveAsync();
                    statusDelegate(availableResponse.QueueStatus);
                }
            });

            var dataflowLinkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            var broadcastBlock = new BroadcastBlock<BufferBlock<QueueResponse>>(x => x);

            _inputBlock.LinkTo(_getQueueResponseBlock, dataflowLinkOptions);
            _getQueueResponseBlock.LinkTo(broadcastBlock, dataflowLinkOptions);


            broadcastBlock.LinkTo(actionBlock, dataflowLinkOptions);
            broadcastBlock.LinkTo(_transformToDeserializedObjectBlock, dataflowLinkOptions);

            await _inputBlock.SendAsync(queueRequestList);
           

            while (await _inputBlock.OutputAvailableAsync())
            {
                await _inputBlock.ReceiveAsync();
            }
           
            while (await _outputBlock.OutputAvailableAsync())
            {
                responseList.Add(await _outputBlock.ReceiveAsync());
            }

            _inputBlock.Complete();
            await Task.WhenAll(_inputBlock.Completion, _outputBlock.Completion);
            return responseList;
        }

        private async Task SendAsync(IEnumerable<BaseQuandlRequest<T>> requests)
        {
            await _inputBlock.SendAsync(requests);
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



















