using NQuandl.Client.Services.TaskQueue;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Client.SimpleInjector.TaskQueue
{
    public class Package : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.RegisterSingleton<ITaskQueue, Services.TaskQueue.TaskQueue>();
        }
    }
}