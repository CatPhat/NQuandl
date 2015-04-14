using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NQuandl.Client;


namespace NQuandl.Queue
{
    public delegate void ActionDelegate<in TResponse>(TResponse quandlResponse) where TResponse : QuandlResponse;

    public delegate Task QueueDelegate();

    public interface IQueueRequest<T> where T : QuandlResponse
    {
        IEnumerable<BaseQuandlRequest<T>> Requests { get; set; }
        ActionDelegate<T> ActionDelegate { get; set; }
    }

    public class QueueRequest<T> : IQueueRequest<T> where T : QuandlResponse
    {
        public IEnumerable<BaseQuandlRequest<T>> Requests { get; set; }
        public ActionDelegate<T> ActionDelegate { get; set; }

    }


    public interface IQuandlRequestQueue<T> where T : QuandlResponse
    {
        Task ConsumeAsync(QueueRequest<T> queueRequest);
    }

    public class QuandlRequestQueue<T> : IQuandlRequestQueue<T> where T : QuandlResponse
    {
        private readonly IDownloadQueue _downloadQueue;
       
        private readonly BufferBlock<BaseQuandlRequest<T>> _inputBlock; 
        private readonly TransformBlock<BaseQuandlRequest<T>, string> _downloadToStringBlock;
      
        private readonly TransformBlock<string, T> _transformToDeserializedObjectBlock;
        private readonly ActionBlock<T> _outputBlock;

        private ActionDelegate<T> _actionDelegate;

        public QuandlRequestQueue(IDownloadQueue downloadQueue)
        {
            _downloadQueue = downloadQueue;

            _inputBlock = new BufferBlock<BaseQuandlRequest<T>>();
            _downloadToStringBlock = new TransformBlock<BaseQuandlRequest<T>, string>(async (x) =>
            {
                var bufferBlock = new BufferBlock<string>();
                bufferBlock.Post(x.Url);
                return await _downloadQueue.AttachToBufferBlock(bufferBlock);
            });
            _transformToDeserializedObjectBlock = new TransformBlock<string, T>(async (x) => await x.DeserializeToObjectAsync<T>());
            _outputBlock = new ActionBlock<T>(response => _actionDelegate(response));
        }

     
        public Task ConsumeAsync(QueueRequest<T> queueRequest)
        {
           return Task.WhenAll(SendAsync(queueRequest), ProcessAsync(), _outputBlock.Completion);
        }

        private async Task SendAsync(QueueRequest<T> queueRequest)
        {
            _actionDelegate = queueRequest.ActionDelegate;
            foreach (var request in queueRequest.Requests)
            {
                await _inputBlock.SendAsync(request);
            }
            _inputBlock.Complete();
        }
       
        private async Task ProcessAsync()
        {
            var dataflowLinkOptions = new DataflowLinkOptions { PropagateCompletion = true };

            _inputBlock.LinkTo(_downloadToStringBlock, dataflowLinkOptions);
            _downloadToStringBlock.LinkTo(_transformToDeserializedObjectBlock, dataflowLinkOptions);
            _transformToDeserializedObjectBlock.LinkTo(_outputBlock, dataflowLinkOptions);

            while (await _inputBlock.OutputAvailableAsync())
            {
                _inputBlock.Receive();
            }
        }
        
    }

   



   


}



















