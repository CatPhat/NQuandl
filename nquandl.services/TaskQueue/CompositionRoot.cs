using SimpleInjector;

namespace NQuandl.Services.TaskQueue
{
    public static class CompositionRoot
    {
        public static void RegisterTaskQueue(this Container container)
        {
            container.RegisterSingleton<ITaskQueue, TaskQueue>();
        }
    }
}