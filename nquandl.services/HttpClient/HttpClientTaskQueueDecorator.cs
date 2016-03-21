using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using JetBrains.Annotations;
using NQuandl.Api.Quandl;
using NQuandl.Domain.Quandl.Responses;
using NQuandl.Services.Logger;
using NQuandl.Services.TaskQueue;

namespace NQuandl.Services.HttpClient
{
    public class HttpClientTaskQueueDecorator : IHttpClient
    {
        private readonly BufferBlock<string> _bufferBlock;
        private readonly Func<IHttpClient> _httpFactory;
        private readonly TransformBlock<string, HttpClientResponse> _transformBlock;


        public HttpClientTaskQueueDecorator([NotNull] Func<IHttpClient> httpFactory, [NotNull] ITaskQueue taskQueue)
        {
            if (httpFactory == null) throw new ArgumentNullException(nameof(httpFactory));
            if (taskQueue == null) throw new ArgumentNullException(nameof(taskQueue));

            _httpFactory = httpFactory;


            _bufferBlock = new BufferBlock<string>();
            _transformBlock =
                new TransformBlock<string, HttpClientResponse>(async item => await _httpFactory().GetAsync(item));

            _bufferBlock.LinkTo(_transformBlock);
        }

        public async Task<HttpClientResponse> GetAsync(string requestUri)
        {
            await _bufferBlock.SendAsync(requestUri);
            NonBlockingConsole.WriteLine("BufferBlockCount: " + _bufferBlock.Count);
            NonBlockingConsole.WriteLine("TransformBlockCount: " + _transformBlock.InputCount);
            var response = await _transformBlock.ReceiveAsync();
            NonBlockingConsole.WriteLine("TransformBlockCount: " + _transformBlock.InputCount);
            return response;

        }
    }
}