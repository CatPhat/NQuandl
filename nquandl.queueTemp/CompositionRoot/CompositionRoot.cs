using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NQuandl.Client;
using SimpleInjector;
using SimpleInjector.Extensions;

namespace NQuandl.Queue
{
    public static class CompositionRoot
    {
        public static void Configure()
        {
            var container = new Container();
            container.ComposeRoot();
        }

        public static void ComposeRoot(this Container container)
        {
            container.Register<IServiceProvider>(() => container, Lifestyle.Singleton);
            container.Register<IQuandlService>(() => new QuandlService());
            container.RegisterOpenGeneric(typeof(IQuandlRequestQueue<>), typeof(QuandlRequestQueue<>));
            container.RegisterSingle<IDownloadQueue>(() => new DownloadQueue());
        
        
       
        }
    }
}
