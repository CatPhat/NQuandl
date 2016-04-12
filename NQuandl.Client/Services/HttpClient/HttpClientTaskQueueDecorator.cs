using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using JetBrains.Annotations;
using NQuandl.Client.Api.Quandl;
using NQuandl.Client.Domain.Responses;
using NQuandl.Client.Services.Logger;
using NQuandl.Client.Services.TaskQueue;

namespace NQuandl.Client.Services.HttpClient
{
    public class HttpClientTaskQueueDecorator : IHttpClient
    {
        private readonly BufferBlock<string> _bufferBlock;
        private readonly Func<IHttpClient> _httpFactory;
        private readonly ILogger _logger;
        private readonly TransformBlock<string, HttpClientResponse> _transformBlock;


        public HttpClientTaskQueueDecorator([NotNull] Func<IHttpClient> httpFactory, [NotNull] ITaskQueue taskQueue,
            [NotNull] ILogger logger)
        {
            if (httpFactory == null) throw new ArgumentNullException(nameof(httpFactory));
            if (taskQueue == null) throw new ArgumentNullException(nameof(taskQueue));
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            _httpFactory = httpFactory;
            _logger = logger;


            _bufferBlock = new BufferBlock<string>();
            _transformBlock =
                new TransformBlock<string, HttpClientResponse>(async item =>
                {
                    
                  
                    var timer = new Stopwatch();
                    timer.Start();
                    var response = await _httpFactory().GetAsync(item);
                    timer.Stop();
                    
                    
                
                    return response;
                });

            _bufferBlock.LinkTo(_transformBlock);
        }

        public async Task<HttpClientResponse> GetAsync(string requestUri)
        {
            await _bufferBlock.SendAsync(requestUri);
            var response = await _transformBlock.ReceiveAsync();
            return response;

        }
    }
}