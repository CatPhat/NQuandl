using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NQuandl.PostgresEF7.Api.Transactions;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.PostgresEF7.SimpleInjector.Transactions.Queries
{
    public class Package : IPackage
    {
        private IEnumerable<Assembly> HandlerAssemblies { get; }

        public Package(params Assembly[] handlerAssemblies)
        {
            if (handlerAssemblies == null || !handlerAssemblies.Any())
            {
                handlerAssemblies = new[] { typeof(IHandleQuery<,>).Assembly };
            }
            HandlerAssemblies = handlerAssemblies;
        }


        public void RegisterServices(Container container)
        {
            container.RegisterSingleton<IExecuteQueries, QueryExecutor>();
            container.Register(typeof(IHandleQuery<,>), HandlerAssemblies);
        }
    }
}