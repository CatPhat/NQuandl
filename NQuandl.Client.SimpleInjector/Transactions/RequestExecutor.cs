using System.Diagnostics;
using JetBrains.Annotations;
using NQuandl.Client.Api.Transactions;
using SimpleInjector;

namespace NQuandl.Client.SimpleInjector.Transactions
{
    [UsedImplicitly]
    internal sealed class RequestExecutor : IExecuteQuandlRequests
    {
        private readonly Container _container;

        public RequestExecutor(Container container)
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