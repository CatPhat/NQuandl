using System;
using NQuandl.Client.Api;
using SimpleInjector;

namespace NQuandl.Client.CompositionRoot
{
    public static class CompositionRoot
    {
        public static void NQuandlRegisterAll(
            this Container container,
            string baseUrl,
            IQuandlRestClient quandlRestClient,
            IQuandlClient quandlClient,
            IProcessQueries queryProcessor)
        {
           
            container.RegisterQuandlRestClient( baseUrl);
            container.RegisterQuandlClient(quandlRestClient);
            container.RegisterQuandlJsonClient(quandlClient, queryProcessor);
            container.RegisterMapper();
            container.RegisterQueries();
        }

        public static IServiceProvider Bootstrap()
        {
            var url = @"https://quandl.com/api";
#if DEBUG
            url = @"http://localhost:49832/api";
#endif
            var container = new Container();
            var quandlRestClient = container.GetInstance<IQuandlRestClient>();
            var quandlClient = container.GetInstance<IQuandlClient>();
            var queryProcessor = container.GetInstance<IProcessQueries>();

            container.NQuandlRegisterAll(url, quandlRestClient, quandlClient, queryProcessor);

            return container.GetInstance<IServiceProvider>();
        }
    }


}