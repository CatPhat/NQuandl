using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Services.Logger;

namespace NQuandl.Services.TaskQueue
{
    public class TaskQueue : ITaskQueue
    {
        private readonly ILogger _logger;
        private readonly SemaphoreSlim _semaphore;

        public TaskQueue([NotNull] ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            _logger = logger;
            _semaphore = new SemaphoreSlim(1);

        }

        public async Task<T> Enqueue<T>(Func<Task<T>> taskGenerator)
        {
            _logger.Write("Enter Semaphore");
            await _semaphore.WaitAsync();
            try
            {
                return await taskGenerator();
            }
            finally
            {
                _logger.Write("SemaphoreCount: " + _semaphore.CurrentCount);
                _semaphore.Release();
            }
        }

        public async Task Enqueue(Func<Task> taskGenerator)
        {
            await _semaphore.WaitAsync();
            try
            {
                await taskGenerator();
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}