using System.Diagnostics;
using JetBrains.Annotations;
using NQuandl.Domain.Persistence.Api.Transactions;
using SimpleInjector;

namespace NQuandl.Services.PostgresEF7.Transactions
{
    [UsedImplicitly]
    internal sealed class QueryExecutor : IExecuteQueries
    {
        private readonly Container _container;

        public QueryExecutor(Container container)
        {
            _container = container;
        }

        [DebuggerStepThrough]
        public TResult Execute<TResult>(IDefineQuery<TResult> query)
        {
            var handlerType = typeof (IHandleQuery<,>).MakeGenericType(query.GetType(), typeof (TResult));
            dynamic handler = _container.GetInstance(handlerType);
            return handler.Handle((dynamic) query);
        }
    }
}