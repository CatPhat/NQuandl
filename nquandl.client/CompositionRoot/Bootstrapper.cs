using System;
using SimpleInjector;

namespace NQuandl.Client.CompositionRoot
{
    public static class Bootstrapper
    {
        private static Container Container;

        public static void Bootstrap(string apiKey = null)
        {
            var url = @"https://quandl.com/api";
#if !DEBUG
            url = @"http://localhost:49832/api";
#endif
            Container = new Container();
            NQuandlRegisterRegisterAll(Container, url, apiKey);
            Container.Verify();
        }

        public static object GetInstance(Type serviceType)
        {
            return Container.GetInstance(serviceType);
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