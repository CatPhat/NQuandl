using NQuandl.Api.Transactions;
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

   


        public static TResult Execute<TResult>(this IDefineQuery<TResult> query)
        {
            return new ExecuteQuery(Container).Execute(query);
        }

      
    }

    public class ExecuteQuery
    {
        private readonly IProcessQueries _queries;

        public ExecuteQuery(Container container)
        {
            _queries = GetQueryProcessor(container);
        }

        public IProcessQueries GetQueryProcessor(Container container)
        {
            return container.GetInstance<IProcessQueries>();
        }

        public TResult Execute<TResult>(IDefineQuery<TResult> query)
        {
            return _queries.Execute(query);
        }

    }
}