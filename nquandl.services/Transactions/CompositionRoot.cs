using System.Reflection;
using NQuandl.Api.Transactions;
using SimpleInjector;

namespace NQuandl.Services.Transactions
{
    public static class CompositionRoot
    {
        public static void RegisterQueryTransactions(this Container container, params Assembly[] assemblies)
        {
            assemblies = assemblies ?? new[] {Assembly.GetAssembly(typeof (IHandleQuandlRequest<,>))};

            container.Register<IProcessQueries, QueryProcessor>(Lifestyle.Singleton);
            container.Register(typeof (IHandleQuandlRequest<,>), assemblies);
        }
    }
}