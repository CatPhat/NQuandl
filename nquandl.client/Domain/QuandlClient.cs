using NQuandl.Client.Api;
using NQuandl.Client.CompositionRoot;

namespace NQuandl.Client.Domain
{
    public class QuandlClient : IQuandlClient
    {
        private readonly string _apiKey;


        public QuandlClient(string apiKey = null)
        {
            _apiKey = apiKey;
        }

        public TResult GetAsync<TResult>(IDefineQuandlQuery<TResult> query)
        {
            var handlerType = typeof (IHandleQuandlQuery<,>).MakeGenericType(query.GetType(), typeof (TResult));

            dynamic handler = Bootstrapper.GetInstance(handlerType);
            return handler.Handle((dynamic) query);
        }
    }
}