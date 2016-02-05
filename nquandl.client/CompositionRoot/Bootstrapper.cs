using System;
using NQuandl.Client.Api;
using SimpleInjector;

namespace NQuandl.Client.CompositionRoot
{
    public static class Bootstrapper
    {
        private static Container Container;

        

        public static void Bootstrap()
        {
            var url = @"https://quandl.com/api";
#if !DEBUG
            url = @"http://localhost:49832/api";
#endif
            Container = new Container();
            NQuandlRegisterRegisterAll(Container, url);
            Container.Verify();
        }

        public static object GetInstance(Type serviceType)
        {
            return Container.GetInstance(serviceType);
        }

        public static void NQuandlRegisterRegisterAll(this Container container, string url)
        {   
            container.RegisterHttpClient(url);
            container.RegisterQueries();
            container.RegisterQuandlRestClient();
            //container.RegisterQuandlClient();
            container.RegisterMapper();
        }
    }

 


}