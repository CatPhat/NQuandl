using System.Reflection;
using NQuandl.Api.Persistence.Transactions;
using NQuandl.Api.Transactions;
using SimpleInjector;

namespace NQuandl.Services.Transactions
{
    public static class CompositionRoot
    {
        public static void RegisterQueryTransactions(this Container container, params Assembly[] assemblies)
        {
            assemblies = assemblies ?? new[] {Assembly.GetAssembly(typeof (IHandleQuandlRequest<,>))};

            container.Register<IExecuteQuandlRequests, RequestExecutor>(Lifestyle.Singleton);
            container.Register(typeof (IHandleQuandlRequest<,>), assemblies);
        }

        public static void RegisterCommandTransactions(this Container container, params Assembly[] assemblies)
        {
            assemblies = assemblies ?? new[] { Assembly.GetAssembly(typeof(IHandleCommand<>)) };

            container.Register<IExecuteCommands, CommandExector>(Lifestyle.Singleton);
            container.Register(typeof(IHandleCommand<>), assemblies);
        }
    }
}