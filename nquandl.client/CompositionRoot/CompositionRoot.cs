using System;
using NQuandl.Client.Api;
using SimpleInjector;

namespace NQuandl.Client.CompositionRoot
{
    public static class CompositionRoot
    {
      
        public static IServiceProvider Bootstrap()
        {
            var url = @"https://quandl.com/api";
#if !DEBUG
            url = @"http://localhost:49832/api";
#endif
            var container = new Container();
            NQuandlRegisterRegisterAll(container, url);
            container.RegisterSingle<IServiceProvider>(() => container);
            return container.GetInstance<IServiceProvider>();
        }

        public static void NQuandlRegisterRegisterAll(this Container container, string url)
        {   
            container.RegisterHttpClient(url);
            container.RegisterQueries();
            container.RegisterQuandlRestClient();
            container.RegisterQuandlClient();
            container.RegisterQuandlJsonClient();
            container.RegisterMapper();
        
        }
    }

 


}