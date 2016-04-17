using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NQuandl.Npgsql.Api.Transactions;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Npgsql.SimpleInjector.Transactions.Commands
{
    public class Package : IPackage
    {
        private IEnumerable<Assembly> HandlerAssemblies { get; }

        public Package(params Assembly[] handlerAssemblies)
        {
            if (handlerAssemblies == null || !handlerAssemblies.Any())
            {
                handlerAssemblies = new[] { typeof(IHandleCommand<>).Assembly };
            }
            HandlerAssemblies = handlerAssemblies;
        }


        public void RegisterServices(Container container)
        {
            container.Register<IExecuteCommands, CommandExecutor>(Lifestyle.Singleton);
            container.Register(typeof(IHandleCommand<>), HandlerAssemblies);
        }
    }
}