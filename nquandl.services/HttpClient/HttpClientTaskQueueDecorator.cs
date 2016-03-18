using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Api.Quandl;
using NQuandl.Domain.Quandl.Responses;
using NQuandl.Services.TaskQueue;

namespace NQuandl.Services.HttpClient
{
    public class HttpClientTaskQueueDecorator : IHttpClient
    {
        private readonly Func<IHttpClient> _httpFactory;
        private readonly ITaskQueue _taskQueue;

        public HttpClientTaskQueueDecorator([NotNull] Func<IHttpClient> httpFactory, [NotNull] ITaskQueue taskQueue)
        {
            if (httpFactory == null) throw new ArgumentNullException(nameof(httpFactory));
            if (taskQueue == null) throw new ArgumentNullException(nameof(taskQueue));
            _httpFactory = httpFactory;
            _taskQueue = taskQueue;
        }

        public Task<HttpClientResponse> GetAsync(string requestUri)
        {
            Console.WriteLine("enqueued");
            return _taskQueue.Enqueue(() => _httpFactory().GetAsync(requestUri));
        }
    }
}