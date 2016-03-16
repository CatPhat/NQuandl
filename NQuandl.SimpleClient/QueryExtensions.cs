using System;
using NQuandl.Api;
using NQuandl.Api.Transactions;
using SimpleInjector;

namespace NQuandl.SimpleClient
{
    public static class QueryExtensions
    {
        static QueryExtensions()
        {
            Bootstrapper.Bootstrap();
        }

        private static IProcessQueries _queries;

        public static void RegisterQueryExtensions(this Container container)
        {
            var queries = container.GetQueryProcessor();
           
            _queries = queries;
        }


        public static TResult Execute<TResult>(this IDefineQuery<TResult> query)
        {
            return _queries.Execute(query);
        }

        public static IProcessQueries GetQueryProcessor(this Container container)
        {
            return container.GetInstance<IProcessQueries>();
        }
    }
}