using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NQuandl.Client.Api.Transactions;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Client.SimpleInjector.Transactions
{
    public class Package : IPackage
    {
        public Package(params Assembly[] handlerAssemblies)
        {
            if (handlerAssemblies == null || !handlerAssemblies.Any())
            {
                handlerAssemblies = new[] {typeof (IHandleQuandlRequest<,>).Assembly};
            }
            HandlerAssemblies = handlerAssemblies;
        }

        private IEnumerable<Assembly> HandlerAssemblies { get; }

        public void RegisterServices(Container container)
        {
            container.Register<IExecuteQuandlRequests, RequestExecutor>(Lifestyle.Singleton);
            container.Register(typeof (IHandleQuandlRequest<,>), HandlerAssemblies);
        }
    }
}