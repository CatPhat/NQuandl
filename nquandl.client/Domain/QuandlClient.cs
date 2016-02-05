using NQuandl.Client.Api;
using NQuandl.Client.CompositionRoot;

namespace NQuandl.Client.Domain
{
    public class QuandlClient : IQuandlClient
    { 
        public TResult GetAsync<TResult>(IDefineQuandlQuery<TResult> query)
        {
            var handlerType = typeof (IHandleQuandlQuery<,>).MakeGenericType(query.GetType(), typeof (TResult));

            dynamic handler = Bootstrapper.GetInstance(handlerType);
            return handler.Handle((dynamic) query);
        }
    }
}