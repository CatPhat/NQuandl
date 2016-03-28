using System.Reflection;
using NQuandl.Api;
using NQuandl.Api.Transactions;
using NQuandl.Domain.Quandl.Requests;
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
            
            //container.RegisterConditional(typeof (IHandleQuery<,>), typeof (HandleQuandlQueryBy<>), c => !c.Handled);
            container.RegisterConditional(typeof (IHandleQuandlRequest<,>), typeof (HandleDatasetByEntity<>), c => !c.Handled);
         


        }
    }
}