using System.Reflection;
using NQuandl.Domain.Persistence.Api.Transactions;
using SimpleInjector;

namespace NQuandl.Services.PostgresEF7.Transactions
{
    public static class CompositionRoot
    {
        public static void RegisterCommandTransactions(this Container container, params Assembly[] assemblies)
        {
            assemblies = assemblies ?? new[] {Assembly.GetAssembly(typeof (IHandleCommand<>))};

            container.Register<IExecuteCommands, CommandExecutor>(Lifestyle.Singleton);
            container.Register(typeof (IHandleCommand<>), assemblies);
        }

        public static void RegisterQueryTransactions(this Container container, params Assembly[] assemblies)
        {
            assemblies = assemblies ?? new[] {Assembly.GetAssembly(typeof (IHandleQuery<,>))};

            container.RegisterSingleton<IExecuteQueries, QueryExecutor>();
            container.Register(typeof (IHandleQuery<,>), assemblies);
        }
    }
}