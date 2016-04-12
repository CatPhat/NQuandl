using System;
using System.Threading.Tasks;

namespace NQuandl.Client.Services.TaskQueue
{
    public interface ITaskQueue
    {
        Task Enqueue(Func<Task> taskGenerator);
        Task<T> Enqueue<T>(Func<Task<T>> taskGenerator);
    }
}