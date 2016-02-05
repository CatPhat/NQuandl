using System;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Api.Helpers;
using NQuandl.Client.CompositionRoot;
using NQuandl.Client.Domain.RequestParameters;

namespace NQuandl.Client.Domain
{
    public class QuandlClient : IQuandlClient
    {
        
        private readonly string _apiKey;
        private readonly IProcessQueries _queries;

        public QuandlClient(string apiKey)
        {
            _apiKey = apiKey;
            _queries = (IProcessQueries)Bootstrapper.GetInstance(typeof(IProcessQueries));
        }

        public TResult GetAsync<TResult>(IDefineQuandlQuery<TResult> query)
        {
            var handlerType = typeof(IHandleQuandlQuery<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = Bootstrapper.GetInstance(handlerType);
            return handler.Handle((dynamic)query);
        }
    }
}