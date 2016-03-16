using System.Reflection;
using NQuandl.Api;
using NQuandl.Domain.Queries;
using SimpleInjector;

namespace NQuandl.Services.Transactions
{
    public static class CompositionRoot
    {
        public static void RegisterQueryTransactions(this Container container, params Assembly[] assemblies)
        {
            assemblies = assemblies ?? new[] {Assembly.GetAssembly(typeof (IHandleQuery<,>))};

            container.RegisterSingleton<IProcessQueries, QueryProcessor>();
            container.Register(typeof (IHandleQuery<,>), assemblies);
            
            container.RegisterConditional(typeof (IHandleQuery<,>), typeof (HandleQuandlQueryBy<>), c => !c.Handled);
            container.RegisterConditional(typeof (IHandleQuery<,>), typeof (HandleDatasetBy<>), c => !c.Handled);
         


        }
    }
}