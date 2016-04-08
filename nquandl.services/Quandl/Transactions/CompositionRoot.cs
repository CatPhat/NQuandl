using System.Reflection;
using NQuandl.Api.Transactions;
using SimpleInjector;

namespace NQuandl.Services.Quandl.Transactions
{
    public static class CompositionRoot
    {
        public static void RegisterQuandlRequestTransactions(this Container container, params Assembly[] assemblies)
        {
            assemblies = assemblies ?? new[] {Assembly.GetAssembly(typeof (IHandleQuandlRequest<,>))};

            container.Register<IExecuteQuandlRequests, RequestExecutor>(Lifestyle.Singleton);
            container.Register(typeof (IHandleQuandlRequest<,>), assemblies);
        }
    }
}