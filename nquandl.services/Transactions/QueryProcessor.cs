using System.Diagnostics;
using JetBrains.Annotations;
using NQuandl.Api;
using NQuandl.Api.Transactions;
using SimpleInjector;

namespace NQuandl.Services.Transactions
{
    [UsedImplicitly]
    sealed class QueryProcessor : IProcessQueries
    {
        private readonly Container _container;

        public QueryProcessor(Container container)
        {
            _container = container;
        }

        [DebuggerStepThrough]
        public TResult Execute<TResult>(IDefineQuandlRequest<TResult> quandlRequest)
        {
            var handlerType = typeof (IHandleQuandlRequest<,>).MakeGenericType(quandlRequest.GetType(), typeof (TResult));
            dynamic handler = _container.GetInstance(handlerType);
            return handler.Handle((dynamic) quandlRequest);
        }

        
    }

   
}