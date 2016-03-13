using NQuandl.Client.Api;
using SimpleInjector;

namespace NQuandl.Client.CompositionRoot
{
    public static class Bootstrapper
    {
        private static Container _container;

        public static Container Configure(Container container = null, string apiKey = null)
        {
            var url = @"https://quandl.com/api";
#if !DEBUG
            url = @"http://localhost:49832/api";
#endif
            var nullContainer = container == null;
            if (nullContainer)
            {
                container = new Container();
            }
            container.NQuandlRegisterRegisterAll(url, apiKey);

            if (nullContainer)
            {
                container.Verify();
            }
            _container = container;
            return container;
        }

        public static IProcessQueries GetQueryProcessor(this Container container)
        {
            return (IProcessQueries) container.GetInstance(typeof (IProcessQueries));
        }

        public static TResult Execute<TResult>(this IDefineQuery<TResult> query)
        {
            if (_container == null)
            {
                _container = new Container();
                Configure(_container);
            }
            var queries = _container.GetQueryProcessor();
            return queries.Execute(query);
        }

        public static void NQuandlRegisterRegisterAll(this Container container, string url, string apiKey = null)
        {
            container.RegisterHttpClient(url);
            container.RegisterQueries();
            container.RegisterQuandlRestClient(apiKey);
            //container.RegisterQuandlClient();
            container.RegisterMapper();
        }
    }
}