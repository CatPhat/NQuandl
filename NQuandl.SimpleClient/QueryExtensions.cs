using NQuandl.Api.Quandl;
using NQuandl.Api.Transactions;
using NQuandl.Services.Quandl;
using SimpleInjector;

namespace NQuandl.SimpleClient
{
    public static class QueryExtensions
    {

        private static readonly Container Container;

        static QueryExtensions()
        {
            Container = Bootstrapper.Bootstrap();
        }

        public static IQuandlClient GetQuandlClient()
        {
            return Container.GetInstance<IQuandlClient>();
        }


        public static TResult Execute<TResult>(this IDefineQuandlRequest<TResult> quandlRequest)
        {
            return new ExecuteQuery(Container).Execute(quandlRequest);
        }

      
    }

    public class ExecuteQuery
    {
        private readonly IExecuteQuandlRequests _queries;

        public ExecuteQuery(Container container)
        {
            _queries = GetQueryProcessor(container);
        }

        public IExecuteQuandlRequests GetQueryProcessor(Container container)
        {
            return container.GetInstance<IExecuteQuandlRequests>();
        }

        public TResult Execute<TResult>(IDefineQuandlRequest<TResult> quandlRequest)
        {
            return _queries.Execute(quandlRequest);
        }

    }
}